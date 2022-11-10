using System.Security.Claims;
using Httpclient.ClientInterfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasm.Auth;

public class CustomAuthProvider : AuthenticationStateProvider
{
    private readonly IUserServices userService;

    public CustomAuthProvider(IUserServices userService)
    {
        this.userService = userService;
        userService.OnAuthStateChanged += AuthStateChanged;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await userService.GetAuthAsync();
        
        return new AuthenticationState(principal);
    }
    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}