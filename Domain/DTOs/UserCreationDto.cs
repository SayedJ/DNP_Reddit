namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    
    public string Role { get; set; }
    
    public int SecurityLevel { get; set; }

    public UserCreationDto(string userName, 
        string email, 
        string password, 
        string rePassword, 
        string firstname, 
        string lastname, 
        string role, 
        int securityLevel
    )
    {
        UserName = userName;
        Email = email;
        Password = password;
        RePassword = rePassword;
        Firstname = firstname;
        Lastname = lastname;
        Role = role;
        SecurityLevel = securityLevel;
    }


}