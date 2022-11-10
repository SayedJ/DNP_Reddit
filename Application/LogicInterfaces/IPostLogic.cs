using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreatePostAsync(PostCreationDto post);
    Task<Post> GetPostByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<Post> GetPostByTitleAsync(string title);
    Task<IEnumerable<Post>> GetAllMyPosts(int id);

    Task DeletePost(int id);


}