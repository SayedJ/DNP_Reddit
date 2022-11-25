using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.DTOs;
using Domain.DTOs;
using Domain.Models;
using Httpclient.ClientInterfaces;

namespace Httpclient.Implementations;

public class CommentHttpClient : ICommentService
{
    private readonly HttpClient client;

    public CommentHttpClient(HttpClient client)
    {
        this.client = client;
    }


    public async Task<List<CommentRetreivingDto>> AllOfThisPostComments(int id)
    {
        var response = await client.GetAsync($"/Comment/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);
        var comments = JsonSerializer.Deserialize<List<CommentRetreivingDto>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comments;

    }

    public async Task<CommentRetreivingDto> CreateCommentAsync(CommentCreationDto dto)
    {
        var response = await client.PostAsJsonAsync("api/Comment/", dto);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) throw new Exception(content);
        var comment = JsonSerializer.Deserialize<CommentRetreivingDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return comment;
    }
}
