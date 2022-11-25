namespace Domain.DTOs;

public class PostRetrievingDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime TimeCreated { get; set; }
    public string Username { get; set; }

   
}