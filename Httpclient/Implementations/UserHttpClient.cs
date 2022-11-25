using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;
using Application;
using Domain.DTOs;
using Domain.Models;
using Httpclient.ClientInterfaces;

namespace Httpclient.Implementations;

public class UserHttpClient : IUserServices

{
    private readonly HttpClient client;
    public static string? Jwt { get; private set; } = "";
    public UserHttpClient(HttpClient client )
    {
        this.client = client;
    }

    
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
    
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }
    private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
    {
        keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
        if (roles != null)
        {
            var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
            if (parsedRoles.Length > 1)
            {
                foreach (var parsedRole in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
            }
            keyValuePairs.Remove(ClaimTypes.Role);
        }
    }
    public async Task<UserInfoRetrieving> CreateUser(UserCreationDto dto)
    {
        var response = await client.PostAsJsonAsync("api/user", dto);
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        UserInfoRetrieving user = JsonSerializer.Deserialize<UserInfoRetrieving>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;

    }

    public async Task<User> GetUser(int id)
    {
        var response = await client.GetAsync($"api/findById/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        User user = JsonSerializer.Deserialize<User>(content, 
            new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;

    }

    public async  Task<IQueryable<UserInfoRetrieving>> GetAllUser()
    {
        var response = await client.GetAsync("api/user");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        var users = JsonSerializer.Deserialize<IQueryable<UserInfoRetrieving>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }

    public async Task LoginUser(string email, string password, bool rememberMe = false)
    {
        LoginModelDto user = new(email, password, rememberMe);
        var response = await client.PostAsJsonAsync("/api/login/", user);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        string token = result;
        Jwt = token;
           // User userRecieved = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
           //  {
           //      PropertyNameCaseInsensitive = true
           //  })!;
           //  
            ClaimsPrincipal principal = CreateClaimsPrincipal();
            OnAuthStateChanged.Invoke(principal);
            
        
        }
    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    public async Task<User> FindByUsername(string username)
    {
        User user = new();
        var response = await client.GetAsync("/api/find/{username}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        user = JsonSerializer.Deserialize<User>(result);
        return user;

    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null;
}