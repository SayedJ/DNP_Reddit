namespace Domain.DTOs;

public class UserRetrievingDto
{
    public string Email { get; set; }
    public string Password { get; set; }

    public UserRetrievingDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}