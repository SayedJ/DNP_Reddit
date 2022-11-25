namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string CommentTitle { get; set; }
    public string CommentDescription { get; set; }
    public User Commentor { get; set; }
    public Post Post { get; set; }

    public Comment(User commentor, string commentTitle, string commentDescription, Post postTitle)
    {
        Commentor = commentor;
        CommentTitle = commentTitle;
        CommentDescription = commentDescription;
        Post = postTitle;
    }

    public Comment()
    {
        
    }
}