namespace Domain.Models;

public class Vote
{
    public int Id { get; set; }
    public bool Liked { get; set; } = true;

    public Post ThisPost { get; set; }
    public User Voter { get; set; }

    public Vote(bool liked, Post thisPost,User voter)
    {
        Liked = liked;
        ThisPost = thisPost;
        Voter = voter;
    }

}