using System.Text.Json;
using Domain.Models;

namespace FileData;

public class FileContext
{
    private const string filePath = "database.json";
    private DataContainer? dataContainer;
    public ICollection<Post> Posts
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Posts;
        }
    }
    public ICollection<User> Users
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Users;
        }
    }
    public ICollection<Comment> Comments
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Comments;
        }
    }
    public ICollection<Vote> Votes
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Votes;
        }
    }
    private void LazyLoadData()
    {
        if (dataContainer == null)
        {
            LoadData();
        }
    }
    private void LoadData()
    {
        if (dataContainer != null) return;
    
        if (!File.Exists(filePath))
        {
            dataContainer = new ()
            {
                Posts = new List<Post>(),
                Users = new List<User>(),
                Comments = new List<Comment>(),
                Votes = new List<Vote>()
            };
            return;
        }
        string content = File.ReadAllText(filePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(filePath, serialized);
        dataContainer = null;
    }
}