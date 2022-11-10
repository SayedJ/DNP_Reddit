using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
    Task<IEnumerable<User>> GetAsync();
    Task<User> LoginAsync(LoginModelDto user);
    Task<User> GetByIdAsync(int id);
    Task<User> FindUserByUsername(string username);

}