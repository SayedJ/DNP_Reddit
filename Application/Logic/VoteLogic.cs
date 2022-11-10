using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class VoteLogic : IVoteLogic
{
    private readonly IVoteDao voteDao;
    private readonly IUserDao userDao;
    private readonly ICommentDao commDao;
    private readonly IPostDao postDao;

    public VoteLogic(IVoteDao voteDao, IUserDao userDao, ICommentDao commDao, IPostDao postDao)
    {
        this.voteDao = voteDao;
        this.userDao = userDao;
        this.commDao = commDao;
        this.postDao = postDao;
    }
    
    public async Task<Vote> CreateAsync(VoteCreationDtos dto)
    {
        User? user = await userDao.GetByIdAsync(dto.VoterId);
        Post? post = await postDao.GetPostAsync(dto.PostId);
        if (user == null && post == null || post != null && user == null || post == null && user != null)
        {
            throw new Exception($"you can't vote");
        }

        if (post.Creator.Id == dto.VoterId)
            throw new Exception("you can't vote for your post");
        var isTrue = await voteDao.IfAlreadyVoted(user.Id, post.Id);
        if (isTrue)
            throw new Exception("You can't vote again");
            
        Vote? vote = new Vote(dto.Liked, post, user);

        Vote toCreate = await voteDao.CreateAsync(vote);
        return toCreate;

    }

    public async Task<IEnumerable<Vote>> GetAllVotes(int postId)
    {
        return await voteDao.GetAllVotesOfThisPosts(postId);
    }
}