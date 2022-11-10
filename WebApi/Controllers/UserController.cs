using System.Security.Claims;
using Application.DaoInterfaces;
using Domain.DTOs;
using Application.LogicInterfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IUserLogic userLogic;
    

    public UserController(IConfiguration config, IUserLogic logic)
    {
        this.config = config;
        userLogic = logic;
    }
    
    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new Claim("DisplayName", user.Firstname),
            new Claim("Email", user.Email),
            new Claim("SecurityLevel", user.SecurityLevel.ToString())
        };
        return claims.ToList();
    }
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync([FromBody]UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("writer")]
    public async Task<ActionResult<IEnumerable<User>>> AllUsers()
    {
        Claim? claim = User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role));
        if (claim == null)
        {
            return StatusCode(403, "You have no role");
        }
        if (!claim.Value.Equals("Writer"))
        {
            return StatusCode(403, "You are not a teacher");
        }
            
        try
        {
            var users = await userLogic.GetAsync();
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpPost]
    [Route("/api/login")]
    public async Task<ActionResult<User>> Login([FromBody]LoginModelDto user)
    {
        try
        {
            User user2 = await userLogic.LoginAsync(user);
            string token = GenerateJwt(user2);
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Can't Login");
        }
    }
    [HttpGet]
    [Route("/api/find/{username}")]
    public async Task<ActionResult<User>> FindByUsername(string username)
    {

        try
        {
            var user = await userLogic.FindUserByUsername(username);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }
    [HttpGet]
    [Route("/api/findById/{id}")]
    public async Task<ActionResult<User>> FindById(int id)
    {

        try
        {
            var user = await userLogic.GetByIdAsync(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
        
    }
    

}