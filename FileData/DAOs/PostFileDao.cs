using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace FileData;

public class PostFileDao : IPostDao
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (context.Posts.Any())
        {
            id = context.Posts.Max(t => t.Id);
            id++;
        }

        post.Id = id;
        context.Posts.Add(post);
        context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<Post> GetPostAsync(int id)
    {
        Post post = context.Posts.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(post);
    }
    
   
    public Task<IEnumerable<Post>> GetAllMyPost(int id)
    {
        var posts = context.Posts.Where(u => u.Creator.Id == id).AsEnumerable();
        return Task.FromResult(posts);
    }

    public Task<IEnumerable<Post>> GetAllPost()
    {
        var posts = context.Posts.AsEnumerable();
        return Task.FromResult(posts);
    }

    public Task<Post> GetOwnerofThePost(int id)
    {
        Post user = context.Posts.FirstOrDefault(u => u.Creator.Id == id);
        return Task.FromResult(user);
    }

    public Task DeletePost(int id)
    {
        var post = context.Posts.FirstOrDefault(c => c.Id == id);
        context.Posts.Remove(post);
        return Task.CompletedTask;
    }

    public Task<Post> GetVotes()
    {
        throw new NotImplementedException();
    }

    

    public Task<Post> GetByTitle(string title)
    {
        Post post = context.Posts.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(post);
    }
}