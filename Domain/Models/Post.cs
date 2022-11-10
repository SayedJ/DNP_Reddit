namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public User Creator { get; set; }
    public int UpVotes { get; set; } = 0;
    public int DownVotes { get; set; } = 0;
    

    public Post(User owner, string title, string description, int upVotes, int downVotes)
    {
        UpVotes = upVotes;
        DownVotes = downVotes;
        Creator = owner;
        Title = title;
        Description = description;
    }

    public Post()
    {
        
    }
    
    
}