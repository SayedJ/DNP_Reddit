using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<IQueryable<UserInfoRetrieving>> GetAsync();
    Task<User> GetUserByUsernameAsync(string username);

    Task<User> LoginAsync(LoginModelDto dto);
    Task<User?> GetByIdAsync(int dtoOwnerId);
    
}