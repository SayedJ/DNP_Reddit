using System.Collections;
using System.Text.Json;
using Domain.Models;
using Httpclient.ClientInterfaces;

namespace Httpclient.Implementations;

public class VoteHttpClient : IVoteService
{
    private readonly HttpClient client;
    public VoteHttpClient(HttpClient client)
    {
        this.client = client;
        
    }

    public Task<Vote> AddVote(bool liked)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Vote>> GetAllVotesonThisPost(int postId)
    {
        var response = await client.GetAsync($"/api/getvotes/{postId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Not a correct request or wrong address");
        }
        var result = await response.Content.ReadAsStringAsync();
        
        var votes = JsonSerializer.Deserialize<IEnumerable<Vote>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return votes;
    }
}