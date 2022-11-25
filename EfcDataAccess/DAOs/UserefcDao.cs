using Application;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class UserefcDao : IUserDao
{   
    private readonly RedditContext context;

    public UserefcDao(RedditContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        User? existngUser =  await context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        return existngUser;
    }

    public async Task<IQueryable<UserInfoRetrieving>> GetAsync()
    {
        var usersDto = from u in context.Users
            select new UserInfoRetrieving()
            {
                id = u.Id,
                Firstname = u.Firstname,
                Email = u.Email,
                Lastname = u.Lastname,
                UserName = u.Username
            };
        var users = await context.Users.ToListAsync();
        return usersDto;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        User? existngUser =  await context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
        return existngUser;
    }




    public async Task<User> LoginAsync(LoginModelDto dto)
    {
        
        User? existing =
            await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(dto.Email) && u.Password.Equals(dto.Password));
        if (existing == null)
        {
            throw new Exception("something is not working");
        }
        return existing;
    }

    public async Task<User> GetByIdAsync(int dtoOwnerId)
    {
        User existing = await context.Users.FindAsync(dtoOwnerId);
        return existing;
    }

    public async Task AddPostToUser(Post obj, int id)
    {
        User user = await  context.Users.FindAsync(id);
        user.Posts.Add(obj);
       await  context.SaveChangesAsync();
    }
    // public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    // {
    //     IQueryable<User> usersQuery = context.Users.AsQueryable();
    //     if (searchParameters.UsernameContains != null)
    //     {
    //         usersQuery = usersQuery.Where(u => u.Username.ToLower().Contains(searchParameters.UsernameContains.ToLower()));
    //     }
    //
    //     IEnumerable<User> result = await usersQuery.ToListAsync();
    //     return result;
    // }
}