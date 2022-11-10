using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : ControllerBase
{
   private readonly IVoteLogic logic;

   public VoteController(IVoteLogic logic)
   {
      this.logic = logic;
   }

   [HttpPost]
   [Route("/api/addVote")]
   public async Task<ActionResult<Vote>> CreateAsync(VoteCreationDtos dto)
   {
      Vote vote = await logic.CreateAsync(dto);
      return Created($"/vote/{vote.Id}", vote);
   }

   [HttpGet]
   [Route("/api/getvotes/{postId}")]
   public async Task<ActionResult<IEnumerable<Vote>>> GetAllCommentsOnThisPost(int postId)
   {
      var votes = await logic.GetAllVotes(postId);
     return Ok(votes);
   }
}