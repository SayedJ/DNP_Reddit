using Domain.Models;

namespace Httpclient.ClientInterfaces;

public interface IVoteService
{
   Task<Vote> AddVote(bool liked);
   Task<IEnumerable<Vote>> GetAllVotesonThisPost(int postId);
}