using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IVoteLogic
{
    Task<Vote> CreateAsync(VoteCreationDtos dto);
    Task<IEnumerable<Vote>> GetAllVotes(int id);

}