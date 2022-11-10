namespace Domain.DTOs;

public class PostRetrievingDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime TimeCreated { get; set; }

    public PostRetrievingDto(string title, string description)
    {
        Description = description;
        TimeCreated = DateTime.Now;
        Title = title;
    }
}