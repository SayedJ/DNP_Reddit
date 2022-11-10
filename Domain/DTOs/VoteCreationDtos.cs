namespace Domain.DTOs;

public class VoteCreationDtos
{
    public int VoterId { get; set; }
    public bool Liked { get; set; }
    public int PostId { get; set; }

    public VoteCreationDtos(int voterId, bool liked, int postId)
    {
        VoterId = voterId;
        Liked = liked;
        PostId = postId;

    }
}