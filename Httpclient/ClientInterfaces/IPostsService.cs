using Domain.DTOs;
using Domain.Models;

namespace Httpclient.ClientInterfaces;

public interface IPostsService
{
  Task<Post> CreatePost(PostCreationDto post);
  Task<Post> GetPost(int id);
  Task<IEnumerable<Post>> GetAllMyPosts(int id);

  Task<IEnumerable<Post>> GetAllPosts();

  Task DeleteAsync(int id);

}