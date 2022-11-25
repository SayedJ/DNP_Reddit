using Application.DTOs;
using Domain.DTOs;
using Domain.Models;

namespace Httpclient.ClientInterfaces;

public interface ICommentService
{

    Task<List<CommentRetreivingDto>> AllOfThisPostComments(int id);
    Task<CommentRetreivingDto> CreateCommentAsync(CommentCreationDto dto);

}