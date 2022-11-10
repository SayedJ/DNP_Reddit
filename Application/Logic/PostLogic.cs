using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;
    private readonly ICommentDao commDao;

    public PostLogic(IPostDao postDao, IUserDao userDao, ICommentDao commDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
        this.commDao = commDao;
       

    }
    
    
    public async Task<Post> CreatePostAsync(PostCreationDto postDto)
    {

        User? existingUser = await userDao.GetByIdAsync(postDto.CreatorId);
        
        if (existingUser == null)
            throw new Exception($"User with id {postDto.CreatorId} was not found");
        ValidData(postDto);
        Post toCreate = new Post
        {
           
            Title = postDto.Title,
            Description = postDto.Description,
            Creator = existingUser
            
            
           

        };
        //Post post = new Post(existingUser, postDto.Title, postDto.Description);
        
        
        Post created = await postDao.CreateAsync(toCreate);
        return created;
    }

    private void ValidData(PostCreationDto postDto)
    {
        if (string.IsNullOrEmpty(postDto.Title)) throw new Exception("Title cannot be empty.");

    }


    public Task<Post> GetPostByIdAsync(int id)
    {
        return postDao.GetPostAsync(id);
    }

    public Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return postDao.GetAllPost();
    }

    public Task<Post> GetPostByTitleAsync(string title)
    {
        return postDao.GetByTitle(title);
    }

    public Task<IEnumerable<Post>> GetAllMyPosts(int id)
    {
        return postDao.GetAllMyPost(id);
    }

    public async Task DeletePost(int id)
    {
        await postDao.DeletePost(id);
    }
}