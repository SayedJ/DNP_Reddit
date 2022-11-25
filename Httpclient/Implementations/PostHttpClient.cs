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
    
    public async Task<PostRetrievingDto> CreatePost(PostCreationDto post)
    {
        var response = await client.PostAsJsonAsync("api/post", post);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        PostRetrievingDto postReturn = JsonSerializer.Deserialize<PostRetrievingDto>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return postReturn;

    }

    public async Task<PostRetrievingDto> GetPost(int id)
    {
        var response = await client.GetAsync($"api/Post/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        PostRetrievingDto post = JsonSerializer.Deserialize<PostRetrievingDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return post;
    }

    public async Task<List<PostRetrievingDto>> GetAllMyPosts(int id)
    {
        var response = await client.GetAsync($"api/myPosts/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var posts = JsonSerializer.Deserialize<List<PostRetrievingDto>>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return posts;
    }

    public async Task<List<PostRetrievingDto>> GetAllPosts()
    {
        var response = await client.GetAsync("api/posts");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var posts = JsonSerializer.Deserialize<List<PostRetrievingDto>>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return posts;
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"api/post/{id}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<int> UpVote(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"api/posts/{id}");
        throw new NotImplementedException();
    }

    public Task<int> DownVote(int id)
    {
        throw new NotImplementedException();
    }
}