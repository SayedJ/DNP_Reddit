using Domain.DTOs;
using Domain.Models;

namespace Httpclient.ClientInterfaces;

public interface ICommentService
{

    Task<IEnumerable<Comment>> AllOfThisPostComments(int id);
    Task<Comment> CreateCommentAsync(CommentCreationDto dto);

}