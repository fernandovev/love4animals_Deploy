using Love4AnimalsApi.Data;
using Love4AnimalsApi.Interfaces;
using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext context;

    public CommentRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await context.Comments
            .AsNoTracking()
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<Comment?> GetCommentByIdAsync(int postId, int id)
    {
        return await context.Comments
            .FirstOrDefaultAsync(c => c.PostId == postId && c.Id == id);
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        context.Comments.Add(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(Comment comment)
    {
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }
}