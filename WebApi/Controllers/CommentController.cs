using Application.DTOs;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentLogic comLogic;

    public CommentController(ICommentLogic comLogic)
    {
        this.comLogic = comLogic;
    }

    [HttpPost]
    public async Task<ActionResult<CommentRetreivingDto>> CreateAsync(CommentCreationDto comment)
    {
        try
        {
            var newComment = await comLogic.CreateAsync(comment);
            return Created($"/Comment/{newComment.Id}", newComment);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/{id}")]
    public async Task<ActionResult<List<CommentRetreivingDto>>> GetCommentsOfThisPost(int id)
    {
        try
        {
            var comments = await comLogic.GetAllCommentsOnThisPost(id);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}