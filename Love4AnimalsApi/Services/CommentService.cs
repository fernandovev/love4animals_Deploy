using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;
    private readonly IPostRepository postRepository;

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this.commentRepository = commentRepository;
        this.postRepository = postRepository;
    }

    public async Task<List<GetCommentDto>> GetCommentsByPostIdAsync(int postId)
    {
        var comments = await commentRepository.GetCommentsByPostIdAsync(postId);

        return comments.Select(c => new GetCommentDto(
            c.Id,
            c.PostId,
            c.Texto,
            c.Fecha
        )).ToList();
    }

    public async Task<GetCommentDto?> GetCommentByIdAsync(int postId, int id)
    {
        var comment = await commentRepository.GetCommentByIdAsync(postId, id);

        if (comment == null)
            return null;

        return new GetCommentDto(
            comment.Id,
            comment.PostId,
            comment.Texto,
            comment.Fecha
        );
    }

    public async Task<GetCommentDto?> CreateCommentAsync(int postId, CreateCommentDto dto)
    {
        var post = await postRepository.GetPostByIdAsync(postId);

        if (post == null)
            return null;

        Comment newComment = new Comment(
            0,
            postId,
            dto.Texto,
            DateTime.Now
        );

        var createdComment = await commentRepository.CreateCommentAsync(newComment);

        return new GetCommentDto(
            createdComment.Id,
            createdComment.PostId,
            createdComment.Texto,
            createdComment.Fecha
        );
    }

    public async Task<GetCommentDto?> UpdateCommentAsync(int postId, int id, UpdateCommentDto dto)
    {
        var comment = await commentRepository.GetCommentByIdAsync(postId, id);

        if (comment == null)
            return null;

        comment.Texto = dto.Texto;
        comment.Fecha = DateTime.Now;

        await commentRepository.UpdateCommentAsync(comment);

        return new GetCommentDto(
            comment.Id,
            comment.PostId,
            comment.Texto,
            comment.Fecha
        );
    }

    public async Task<GetCommentDto?> DeleteCommentAsync(int postId, int id)
    {
        var comment = await commentRepository.GetCommentByIdAsync(postId, id);

        if (comment == null)
            return null;

        var deletedComment = new GetCommentDto(
            comment.Id,
            comment.PostId,
            comment.Texto,
            comment.Fecha
        );

        await commentRepository.DeleteCommentAsync(comment);

        return deletedComment;
    }
}