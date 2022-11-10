namespace Domain.DTOs;

public class LoginModelDto
{
  
    public string Email { get; set; }
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
   
    
    public LoginModelDto(string email, string password, bool rememberMe)
    {
      Email = email;
      RememberMe = rememberMe;
      Password = password;
     
    }
}