using System.ComponentModel.DataAnnotations;

namespace Application;

public class UserInfoRetrieving
{
    [Key]
    public int id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Lastname { get; set; }
    public string Firstname { get; set; }

    public UserInfoRetrieving(string userName, 
        string email,
        string firstname, 
        string lastname

    )
    {
        UserName = userName;
        Email = email;
        Firstname = firstname;
        Lastname = lastname;
    }

    public UserInfoRetrieving()
    {
    }
}