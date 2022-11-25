using Application.DTOs;
using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<CommentRetreivingDto> CreateAsync(CommentCreationDto dto);
    Task<Comment> GetCommentByUserId(int id);
    Task<List<Comment>> GetAllComments();
    Task<List<CommentRetreivingDto>> GetAllCommentsOnThisPost(int id);
}