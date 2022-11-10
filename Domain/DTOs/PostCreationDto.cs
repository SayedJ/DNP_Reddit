namespace Domain.DTOs;

public class PostCreationDto
{
    public int CreatorId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
  

    public PostCreationDto(int creatorId, string title, string description)
    {
        
        CreatorId = creatorId;
        Title = title;
        Description = description;
   


    }
    
}