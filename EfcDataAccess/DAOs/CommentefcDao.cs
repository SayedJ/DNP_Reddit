using Application.DaoInterfaces;
using Application.DTOs;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class CommentefcDao : ICommentDao
{
    private readonly RedditContext context;

    public CommentefcDao(RedditContext context)
    {
        this.context = context;
    }

    public async Task<CommentRetreivingDto> CreateAsync(CommentCreationDto comment)
    {
        User? user = await context.Users.FindAsync(comment.CommentorId);
        Post? post = await context.Posts.FindAsync(comment.PostId);
     
        if (user == null)
            throw new Exception($"User {comment.CommentorId} can not be found!");
        if (post == null)
            throw new Exception($"Post {comment.PostId} can not be found");
        Comment commentToDB = new Comment()
        {
          
            CommentTitle = comment.CommentTitle,
            CommentDescription = comment.CommentDescription,
            Commentor = user,
            Post = post
        };
        EntityEntry<Comment> addedComment = await  context.Comments.AddAsync(commentToDB);
        await context.SaveChangesAsync();
        var newComment = new CommentRetreivingDto(){
            Id = addedComment.Entity.Id,
                Title = addedComment.Entity.CommentTitle,
            Description = addedComment.Entity.CommentDescription,
            Username = addedComment.Entity.Commentor.Username,
            Post = addedComment.Entity.Post.Title
        };


        return newComment;
    }

    public async Task<Comment> GetCommentAsync(int id)
    {
        var comment = await context.Comments.FindAsync(id);
        return comment;
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        var comments = await context.Comments.ToListAsync();
        return comments;
    }

    public async Task<List<CommentRetreivingDto>> GetAllCommentsOnThisPostAsync(int id)
    {
        // var findingAllComments = await context.Comments.Where(b => b.Post.Id == id).ToListAsync();
        var comments = await (from b in  context.Comments
            where b.Post.Id == id
            select new CommentRetreivingDto()
            {
                Title = b.CommentTitle,
                Description = b.CommentDescription,
                Username = b.Commentor.Username,
                Post = b.Post.Title
            }).ToListAsync();

        return comments;
    }

    public async Task DeleteComment(int id)
    {
        var comment = await context.Comments.FindAsync(id);
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Comment comment)
    {
        context.ChangeTracker.Clear();
        context.Comments.Update(comment);
        await context.SaveChangesAsync();
    }
}