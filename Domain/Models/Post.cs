using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Application;
using Newtonsoft.Json;


namespace Domain.Models;

public class Post
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Title { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
    public User Creator { get; set; }
    public int UpVotes { get; set; } = 0;
    public int DownVotes { get; set; } = 0;
    
    public List<Comment> Comments { get; init; }
    

    public Post(User owner, string title, string description, int upVotes, int downVotes)
    {
        UpVotes = upVotes;
        DownVotes = downVotes;
        Creator = owner;
        Title = title;
        Description = description;
        Comments = new List<Comment>();

    }
    
    public Post()
    {
        
    }
}