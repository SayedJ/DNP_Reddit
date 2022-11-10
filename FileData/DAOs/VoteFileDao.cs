
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData;

public class VoteFileDao : IVoteDao
{
    private readonly FileContext context;

    public VoteFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Vote> CreateAsync(Vote vote)
    {
        int id = 1;
        if (context.Votes.Any())
        {
            id = context.Votes.Max(c => c.Id);
            id++;
        }

        vote.Id = id;
        context.Votes.Add(vote);
        context.SaveChanges();
        return Task.FromResult(vote);
    }

    public Task<bool> IfAlreadyVoted(int userId, int postId)
    {
        bool isTrue = context.Votes.Where(c => c.Voter.Id == userId && c.ThisPost.Id == postId).Any();
        return Task.FromResult(isTrue);

    }

    public Task<IEnumerable<Vote>> GetAllVotesOfThisPosts(int postId)
    {
        
        var votes = context.Votes.Where(c => c.ThisPost.Id == postId);
        
        


        return Task.FromResult(votes);
    }
}