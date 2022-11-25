using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
 Task<PostRetrievingDto> CreateAsync(Post post);
 Task<PostRetrievingDto> GetPostAsync(int id);
 Task<List<PostRetrievingDto>> GetAllMyPost(int id);
 Task<List<PostRetrievingDto>> GetAllPost();
 Task DeletePost(int id);
 // Task<Post> GetByTitle(string title);
 Task<int> AddVote(int id);
 Task<int> DownVote(int id);
 Task UpdatePost(Post postToUpdate);


}