using Domain.DTOs;
using Domain.Models;

namespace Httpclient.ClientInterfaces;

public interface IPostsService
{
  Task<PostRetrievingDto> CreatePost(PostCreationDto post);
  Task<PostRetrievingDto> GetPost(int id);
  Task<List<PostRetrievingDto>> GetAllMyPosts(int id);

  Task<List<PostRetrievingDto>> GetAllPosts();

  Task DeleteAsync(int id);

  Task<int> UpVote(int id);
  Task<int> DownVote(int id);

}