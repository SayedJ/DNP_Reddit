namespace Domain.DTOs;

public class CommentCreationDto
{
    public int CommentorId { get; set; }
    public string CommentTitle { get; set; }
    public string CommentDescription { get; set; }
    public int PostId { get; set; }
    

    public CommentCreationDto(int commentorId, string commentTitle, string commentDescription, int postId)
    {
        CommentorId = commentorId;
        CommentTitle = commentTitle;
        CommentDescription = commentDescription;
        PostId = postId;
    }

    public CommentCreationDto()
    {
        
    }
}