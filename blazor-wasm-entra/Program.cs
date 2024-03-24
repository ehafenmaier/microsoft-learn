using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasmEntraID;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    // Add default token scopes
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("https://graph.microsoft.com/user.read");
    
    // Change login mode to redirect
    options.ProviderOptions.LoginMode = "redirect";
});

await builder.Build().RunAsync();
