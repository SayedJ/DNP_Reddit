using System.Security.Claims;
using Application;
using Domain.DTOs;
using Domain.Models;

namespace Httpclient.ClientInterfaces;

public interface IUserServices
{
    Task<UserInfoRetrieving> CreateUser(UserCreationDto dto);
    Task<User> GetUser(int id);
    Task<IQueryable<UserInfoRetrieving>> GetAllUser();

    Task LoginUser(string email, string password, bool rememberMe);
    Task LogoutAsync();

    Task<User>FindByUsername(string username);

    Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}