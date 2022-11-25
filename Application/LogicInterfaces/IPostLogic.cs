using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<PostRetrievingDto> CreatePostAsync(PostCreationDto post);
    Task<PostRetrievingDto> GetPostByIdAsync(int id);
    Task<List<PostRetrievingDto>> GetAllPostsAsync();
    // Task<Post> GetPostByTitleAsync(string title);
    Task<List<PostRetrievingDto>> GetAllMyPosts(int id);

    Task DeletePost(int id);

    Task<int> AddVote(int id);
    Task<int> DownVote(int id);


}