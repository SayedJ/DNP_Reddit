using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<Comment> CreateAsync(CommentCreationDto dto);
    Task<Comment> GetCommentByUserId(int id);
    Task<IEnumerable<Comment>> GetAllComments();
    Task<IEnumerable<Comment>> GetAllCommentsOnThisPost(int id);
}