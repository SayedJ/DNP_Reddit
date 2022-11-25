using System.Runtime.CompilerServices;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostefcDao : IPostDao
{
    private readonly RedditContext context;

    public PostefcDao(RedditContext context)
    {
        this.context = context;
    }

    public async Task<PostRetrievingDto> CreateAsync(Post post)
    {
      
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        var addedPost = new PostRetrievingDto()
            {   Id = added.Entity.Id,
                Description = added.Entity.Description,
                TimeCreated = DateTime.Now,
                Title = added.Entity.Title,
                Username = added.Entity.Creator.Username
            };
        await context.SaveChangesAsync();
        return addedPost;
    }

    public async Task<PostRetrievingDto> GetPostAsync(int id)
    {
        Post foundPost = await context.Posts.FindAsync(id);
        var addedPost = new PostRetrievingDto()
        {   Id = foundPost.Id,
            Description = foundPost.Description,
            TimeCreated = DateTime.Now,
            Title = foundPost.Title,
            Username = foundPost.Creator.Username
        };
        return addedPost;
    }

    public async Task<List<PostRetrievingDto>> GetAllMyPost(int id)
    {
        
        var posts = await context.Posts.Where(b => b.Creator.Id == id).ToListAsync();
        var postToShow = await (
                from p in context.Posts
                where p.Creator.Id == id 
                select new PostRetrievingDto()
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Username = p.Creator.Username,
                TimeCreated = DateTime.Now
            }).ToListAsync();
        return postToShow;
    }

    public async Task<List<PostRetrievingDto>> GetAllPost()
    {
        var posts = await ( from b in context.Posts
        select new PostRetrievingDto()
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            Username = b.Creator.Username
        }).ToListAsync();
        return posts;
    }
    

    // public async Task<Post> GetOwnerofThePost(int id)
    // {
    //     Post post = await context.Posts.FirstOrDefaultAsync(c => c.Creator.ToLower().Equals(username.ToLower()));
    //     return post;
    // }

    public async Task DeletePost(int id)
    {
        Post? post = await context.Posts.AsNoTracking().Include(post => post.Comments).SingleOrDefaultAsync(c => c.Id==id); 
        context.Posts.Remove(post);
        context.SaveChanges();
    }

    // public async Task<PostRetrievingDto> GetByTitle(string title)
    // {
    //     Post? post = await context.Posts.FirstOrDefaultAsync(c => c.Title.ToLower().Equals(title.ToLower()));
    //     return post;
    // }

    public async Task<int> AddVote(int id)
    {
        Post? post = await context.Posts.FirstOrDefaultAsync(c => c.Id == id);
        post.UpVotes++;
        return post.UpVotes;
    }

    public async Task<int> DownVote(int id)
    {
        Post? post = await context.Posts.FirstOrDefaultAsync(c => c.Id == id);
        post.DownVotes++;
        return post.DownVotes;
    }

    public async Task UpdatePost(Post postToUpdate)
    {
        Post? post = await context.Posts.FindAsync(postToUpdate.Id);
        post = postToUpdate;
        context.Update(post);
        context.SaveChangesAsync();


    }
}