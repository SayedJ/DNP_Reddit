using Domain.Models;

namespace Domain.DTOs;

public class VoteRetrevingDto
{
    public int UpVotes { get; set; } = 0;

    public int DownVotes { get; set; } = 0;
    public int PostId { get; set; }

    public VoteRetrevingDto(int upVotes, int downVotes, int postId)
    {
        UpVotes = upVotes;
        DownVotes = downVotes;
        PostId = postId;

    }

    public VoteRetrevingDto()
    {
     
    }
}