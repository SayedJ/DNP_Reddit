using Application.DaoInterfaces;
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
        
    
    
    
    public async Task<Comment> CreateAsync(CommentCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.CommentorId);
        Post? post = await postDao.GetPostAsync(dto.PostId);
     
        if (user == null)
            throw new Exception($"User {dto.CommentorId} can not be found!");
        if (post == null)
            throw new Exception($"Post {dto.PostId} can not be found");
        Comment inPutComment = new Comment(user, dto.CommentTitle, dto.CommentDescription, post);
        Comment newComment = new Comment{
            CommentDescription = dto.CommentDescription,
            Commentor = user,
            CommentTitle = dto.CommentTitle,
            OnPost = post
            
            
        };
        var toCreate = await commDao.CreateAsync(newComment);
        return toCreate;
    }

    public async Task<Comment> GetCommentByUserId(int id)
    {
        return await commDao.GetCommentAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsOnThisPost(int id)
    {
        return await commDao.GetAllCommentsOnThisPostAsync(id);
    }


    public async Task<IEnumerable<Comment>> GetAllComments()
    {
        return await commDao.GetAllCommentsAsync();
    }
}