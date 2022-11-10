using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using Httpclient.ClientInterfaces;

namespace Httpclient.Implementations;

public class PostHttpClient : IPostsService
{
    private readonly HttpClient client;
    private readonly IUserServices _userServices;
    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Post> CreatePost(PostCreationDto post)
    {
        var response = await client.PostAsJsonAsync("api/post", post);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        Post postReturn = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return postReturn;

    }

    public async Task<Post> GetPost(int id)
    {
        var response = await client.GetAsync($"api/Post/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        Post post = JsonSerializer.Deserialize<Post>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return post;
    }

    public async Task<IEnumerable<Post>> GetAllMyPosts(int id)
    {
        var response = await client.GetAsync($"api/myPosts/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var posts = JsonSerializer.Deserialize<IEnumerable<Post>>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return posts;
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        var response = await client.GetAsync($"api/posts/");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var posts = JsonSerializer.Deserialize<IEnumerable<Post>>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return posts;
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"api/posts/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}