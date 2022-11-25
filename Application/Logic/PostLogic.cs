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
    
    
    public async Task<PostRetrievingDto> CreatePostAsync(PostCreationDto postDto)
    {

        User existingUser = await userDao.GetByIdAsync(postDto.CreatorId);
        
        if (existingUser == null)
            throw new Exception($"User with id {postDto.CreatorId} was not found");
        ValidData(postDto);
       Post toAdd = new Post
       {   
           Comments = new List<Comment>(),
           Creator= existingUser, 
           Title = postDto.Title,
           Description = postDto.Description,
           DownVotes = 0,
           UpVotes = 0,
           
       };
       
       PostRetrievingDto created = await postDao.CreateAsync(toAdd);
       return created;
    }

    private void ValidData(PostCreationDto postDto)
    {
        if (string.IsNullOrEmpty(postDto.Title)) throw new Exception("Title cannot be empty.");

    }


    public Task<PostRetrievingDto> GetPostByIdAsync(int id)
    {
        return postDao.GetPostAsync(id);
    }

    public Task<List<PostRetrievingDto>> GetAllPostsAsync()
    {
        return postDao.GetAllPost();
    }

    // public Task<PostRetrievingDto> GetPostByTitleAsync(string title)
    // {
    //     return postDao.GetByTitle(title);
    // }

    public Task<List<PostRetrievingDto>> GetAllMyPosts(int id)
    {
        return postDao.GetAllMyPost(id);
    }

    public async Task DeletePost(int id)
    {
        await postDao.DeletePost(id);
    }

    public async Task<int> AddVote(int id)
    {
        return await postDao.AddVote(id);
    }

    public async Task<int> DownVote(int id)
    {
        return await postDao.DownVote(id);
    }
    
}