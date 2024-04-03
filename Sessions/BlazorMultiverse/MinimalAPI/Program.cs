using BlazorWebAssembly.Server.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapBlogPostEndpoints();
app.MapCategoryEndpoints();
app.MapTagEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();
