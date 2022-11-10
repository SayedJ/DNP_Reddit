using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
 Task<Post> CreateAsync(Post post);
 Task<Post> GetPostAsync(int id);
 Task<IEnumerable<Post>> GetAllMyPost(int id);
 Task<IEnumerable<Post>> GetAllPost();
 Task<Post> GetOwnerofThePost(int id);

 Task DeletePost(int id);
 Task<Post> GetByTitle(string title);
 

}