using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
