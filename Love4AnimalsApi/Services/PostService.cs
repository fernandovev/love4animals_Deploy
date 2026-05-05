using Love4AnimalsApi.Dtos;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;

namespace Love4AnimalsApi.Services;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;

    public PostService(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task<List<GetPostDto>> GetPostsAsync()
    {
        var posts = await postRepository.GetPostsAsync();

        return posts.Select(p => new GetPostDto(
            p.Id,
            p.Titulo,
            p.MetaRecaudacion,
            p.MontoActual,
            p.Estado,
            p.Imagen,
            p.Descripcion,
            p.FechaCreacion,
            p.CantidadLikes,
            p.CantidadComentarios,
            p.CantidadCompartidos
        )).ToList();
    }

    public async Task<GetPostDto?> GetPostByIdAsync(int id)
    {
        var post = await postRepository.GetPostByIdAsync(id);

        if (post == null)
            return null;

        return new GetPostDto(
            post.Id,
            post.Titulo,
            post.MetaRecaudacion,
            post.MontoActual,
            post.Estado,
            post.Imagen,
            post.Descripcion,
            post.FechaCreacion,
            post.CantidadLikes,
            post.CantidadComentarios,
            post.CantidadCompartidos
        );
    }

    public async Task<GetPostDto> CreatePostAsync(CreatePostDto dto)
    {
        Post newPost = new Post(
            0,
            dto.Titulo,
            dto.MetaRecaudacion,
            dto.MontoActual,
            dto.Estado,
            dto.Imagen,
            dto.Descripcion,
            DateTime.UtcNow,
            0,
            0,
            0
        );

        var createdPost = await postRepository.CreatePostAsync(newPost);

        return new GetPostDto(
            createdPost.Id,
            createdPost.Titulo,
            createdPost.MetaRecaudacion,
            createdPost.MontoActual,
            createdPost.Estado,
            createdPost.Imagen,
            createdPost.Descripcion,
            createdPost.FechaCreacion,
            createdPost.CantidadLikes,
            createdPost.CantidadComentarios,
            createdPost.CantidadCompartidos
        );
    }

    public async Task<GetPostDto?> UpdatePostAsync(int id, UpdatePostDto dto)
    {
        var post = await postRepository.GetPostByIdAsync(id);

        if (post == null)
            return null;

        post.Titulo = dto.Titulo;
        post.MetaRecaudacion = dto.MetaRecaudacion;
        post.MontoActual = dto.MontoActual;
        post.Estado = dto.Estado;
        post.Imagen = dto.Imagen;
        post.Descripcion = dto.Descripcion;

        await postRepository.UpdatePostAsync(post);

        return new GetPostDto(
            post.Id,
            post.Titulo,
            post.MetaRecaudacion,
            post.MontoActual,
            post.Estado,
            post.Imagen,
            post.Descripcion,
            post.FechaCreacion,
            post.CantidadLikes,
            post.CantidadComentarios,
            post.CantidadCompartidos
        );
    }

    public async Task<GetPostDto?> DeletePostAsync(int id)
    {
        var post = await postRepository.GetPostByIdAsync(id);

        if (post == null)
            return null;

        var deletedPost = new GetPostDto(
            post.Id,
            post.Titulo,
            post.MetaRecaudacion,
            post.MontoActual,
            post.Estado,
            post.Imagen,
            post.Descripcion,
            post.FechaCreacion,
            post.CantidadLikes,
            post.CantidadComentarios,
            post.CantidadCompartidos
        );

        await postRepository.DeletePostAsync(post);

        return deletedPost;
    }
}