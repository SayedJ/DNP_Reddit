using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;


    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<PostRetrievingDto>> CreatePostAsync(PostCreationDto dto)
    {
        try
        {
            PostRetrievingDto? post = await postLogic.CreatePostAsync(dto);
            return Created($"/post/{post.Id}", post);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);

        }

    }

    [HttpGet]
    [Route("/[controller]/{id}")]
    public async Task<ActionResult<PostRetrievingDto>> GetPostById(int id)
    {
        try
        {
            PostRetrievingDto post = await postLogic.GetPostByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("The post you are looking for, can not be found!");
        }
    }

    // [HttpGet]
    // [Route("/api/{title}")]
    // public async Task<ActionResult<Post>> GetPostByTitle(string title)
    // {
    //     try
    //     {
    //         Post post = await postLogic.GetPostByTitleAsync(title);
    //         return Ok(post);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         throw new Exception($"The post with title {title}, can not be found!");
    //     }
    // }

    [HttpGet]
    [Route("/api/myPosts/{id}")]
    public async Task<ActionResult<List<PostRetrievingDto>>> GetAllMyPosts(int id)
    {
        try
        {
            var posts = await postLogic.GetAllMyPosts(id);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"No posts were found or the creator with {id}, can not be found!");
        }
    }

    [HttpGet]
    [Route("/api/posts/")]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        try
        {
            var posts = await postLogic.GetAllPostsAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("No posts were found!");

        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost([FromRoute]int id)
    {
        try
        {
            await postLogic.DeletePost(id);
            return Ok("The post has been delete.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    [Route("/api/posts/upvote")]
    public async Task<ActionResult> UpVote(int id)
    {
        try
        {
           int upvote = await postLogic.AddVote(id);
            return Ok(upvote);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPost]
    [Route("/api/posts/downvote")]
    public async Task<ActionResult> DownVote(int id)
    {
        try
        {
            int downVote = await postLogic.DownVote(id);
            return Ok(downVote);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}