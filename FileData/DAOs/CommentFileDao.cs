using Application.DaoInterfaces;
using Domain.Models;


namespace FileData;

public class CommentFileDao : ICommentDao
{
    private readonly FileContext context;

    public CommentFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Comment> CreateAsync(Comment comment)
    {
        
        
        
        
        int id = 1;
        if (context.Comments.Any())
        {
            id = context.Comments.Max(t => t.Id);
            id++;
        }

        comment.Id = id;
        context.Comments.Add(comment);
        context.SaveChanges();
        return Task.FromResult(comment);
    }
 

    public Task<Comment> GetCommentAsync(int id)
    {
        Comment? comment = context.Comments.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(comment);
    }

    public Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        var comments = context.Comments.AsEnumerable();
        return Task.FromResult(comments);
    }

    public Task<IEnumerable<Comment>> GetAllCommentsOnThisPostAsync(int id)
    {
        var comments = context.Comments.Where(c => c.OnPost.Id == id);
        return Task.FromResult(comments);
    }
    
}