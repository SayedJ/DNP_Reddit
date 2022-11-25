using Application.DTOs;
using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface ICommentDao
{
    Task<CommentRetreivingDto> CreateAsync(CommentCreationDto comment);
    Task<Comment> GetCommentAsync(int id);
    Task<List<Comment>> GetAllCommentsAsync();
    Task<List<CommentRetreivingDto>> GetAllCommentsOnThisPostAsync(int id);

}