using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Role { get; set; }
    
    public ICollection<Post> Posts { get; set; }
    public int SecurityLevel { get; set; }
    
}