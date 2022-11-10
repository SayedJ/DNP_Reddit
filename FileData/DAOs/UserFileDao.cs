
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;


namespace FileData;

public class UserFileDao : IUserDao
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int id = 1;
        if (context.Users.Any())
        {
            id = context.Users.Max(u => u.Id);
            id++;
        }

        user.Id = id;
        context.Users.Add(user);
        context.SaveChanges();
        return Task.FromResult(user);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }
    
   
    public Task<IEnumerable<User>> GetAsync()
    {
        var allUsers = context.Users.AsEnumerable();
        return Task.FromResult(allUsers);
    }

    public Task<User> GetUserByUsernameAsync(string username)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<User> LoginAsync(LoginModelDto user)
    {
        
        User? existing = context.Users.FirstOrDefault(u =>
            u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase) 
            && u.Password.Equals(user.Password));
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(int dtoOwnerId)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == dtoOwnerId);
        return Task.FromResult(existing);
    }
}

