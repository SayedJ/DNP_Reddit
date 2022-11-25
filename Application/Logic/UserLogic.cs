using Application.DaoInterfaces;
using Domain.DTOs;
using Application.LogicInterfaces;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;
    

   
     

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
        


    
    public async Task<User> CreateAsync(UserCreationDto userCreation)
    {
        User? existing = await userDao.GetByEmailAsync(userCreation.Email);
        User? existingUser = await userDao.GetUserByUsernameAsync(userCreation.UserName);
        if (existing != null && existingUser != null)
            throw new Exception($"{existingUser.Username}, {existing.Email} is already taken");
        ValidData(userCreation);
        User toCreate = new User
        {
            Email = userCreation.Email,
            Username = userCreation.UserName,
            Password = userCreation.Password,
            RePassword = userCreation.RePassword,
            Firstname = userCreation.Firstname,
            Lastname = userCreation.Lastname,
            Role = userCreation.Role,
            SecurityLevel = userCreation.SecurityLevel


        };
        User created = await userDao.CreateAsync(toCreate);
     
        return created;

    }

    private static void ValidData(UserCreationDto userDto)
    {
        string password = userDto.Password;
        string rePassword = userDto.RePassword;
        string username = userDto.UserName;
        string email = userDto.Email;
        
        if (username.Length < 3 && email.Length < 7)
            throw new Exception("Username and email must be more than 3 letters");
        if(username.Length> 15)
            throw new Exception("Username must be less than 15 letters");
        if (!password.Equals(rePassword))
            throw new Exception("Password and Re-Password must match");
    }

    public Task<IQueryable<UserInfoRetrieving>> GetAsync()
    {
        return userDao.GetAsync();
    }
    public Task<User> GetByIdAsync(int id)
    {
        return userDao.GetByIdAsync(id);
    }


    public Task<User> LoginAsync(LoginModelDto user)
    {
        return userDao.LoginAsync(user);
    }

    public Task<User> FindUserByUsername(string username)
    {
        return userDao.GetUserByUsernameAsync(username);
    }

    public Task<User> UserByIdAsync(int id)
    {
        return userDao.GetByIdAsync(id);
    }

    public Task<User> UserByEmailAsync(string email)
    {
        return userDao.GetByEmailAsync(email);
    }
    public Task<User> UserByUsernameAsync(string username)
    {
        return userDao.GetUserByUsernameAsync(username);
    }

  
}