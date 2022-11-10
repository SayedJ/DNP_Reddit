using System.Security.Claims;
using Domain.DTOs;
using Domain.Models;

namespace Httpclient.ClientInterfaces;

public interface IUserServices
{
    Task<User> CreateUser(UserCreationDto dto);
    Task<User> GetUser(int id);
    Task<IEnumerable<User>> GetAllUser();

    Task LoginUser(string email, string password, bool rememberMe);
    Task LogoutAsync();

    Task<User>FindByUsername(string username);

    Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}