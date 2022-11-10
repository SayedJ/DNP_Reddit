namespace Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string CommentTitle { get; set; }
    public string CommentDescription { get; set; }
    public User Commentor { get; set; }
    public Post OnPost { get; set; }

    public Comment(User commentor, string commentTitle, string commentDescription, Post onPost)
    {
        Commentor = commentor;
        CommentTitle = commentTitle;
        CommentDescription = commentDescription;
        OnPost = onPost;
    }

    public Comment()
    {
        
    }
}