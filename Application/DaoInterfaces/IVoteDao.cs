using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IVoteDao
{
    Task<Vote> CreateAsync(Vote vote);
    Task<bool> IfAlreadyVoted(int userId, int postId);

    Task<IEnumerable<Vote>> GetAllVotesOfThisPosts(int postId);
}