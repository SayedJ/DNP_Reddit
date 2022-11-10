using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasm;
using BlazorWasm.Auth;
using BlazorWasm.ModelsToPass;
using Httpclient.ClientInterfaces;
using Httpclient.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IUserServices , UserHttpClient>();
builder.Services.AddScoped<IPostsService , PostHttpClient>();
builder.Services.AddScoped<ICommentService , CommentHttpClient>();
builder.Services.AddScoped<IVoteService , VoteHttpClient>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy("PostWriter", policy => policy.RequireClaim("Roles", "Writer", "Admin", "Viewer"));
});



builder.Services.AddScoped<AppState>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7214") });
await builder.Build().RunAsync();