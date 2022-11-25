using Application.DaoInterfaces;
using Application.DTOs;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CommentLogic : ICommentLogic
{
    private readonly IUserDao userDao;
    private readonly ICommentDao commDao;
    private readonly IPostDao postDao;

    public CommentLogic(IPostDao postDao, IUserDao userDao, ICommentDao commDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
        this.commDao = commDao;

    }
        
    
    
    
    public async Task<CommentRetreivingDto> CreateAsync(CommentCreationDto dto)
    {
        var toCreate = await commDao.CreateAsync(dto);
        return toCreate;
    }

    public async Task<Comment> GetCommentByUserId(int id)
    {
        return await commDao.GetCommentAsync(id);
    }

    public async Task<List<CommentRetreivingDto>> GetAllCommentsOnThisPost(int id)
    {
        return await commDao.GetAllCommentsOnThisPostAsync(id);
    }


    public async Task<List<Comment>> GetAllComments()
    {
        return await commDao.GetAllCommentsAsync();
    }
}