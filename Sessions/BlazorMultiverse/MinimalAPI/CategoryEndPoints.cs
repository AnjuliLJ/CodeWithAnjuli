using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Server.Endpoints
{
    public static class CategoryEndPoints
    {
        private static List<Category> _categories = new()
        {
            new Category() { Id = Guid.NewGuid().ToString(), Name = "Blazor"},
            new Category() { Id = Guid.NewGuid().ToString(), Name = ".NET" }
        };
        
        public static void MapCategoryEndpoints(this WebApplication app)
        {
            {
                app.MapGet("/api/Categories",
                () =>
                {
                    return Results.Ok(_categories);
                });

                app.MapGet("/api/Categories/{*id}",
                (string id) =>
                {
                    return Results.Ok(_categories.FirstOrDefault(x => x.Id == id));
                });

                app.MapDelete("/api/Categories/{*id}",
                (string id) =>
                {
                    var category = _categories.FirstOrDefault(x => x.Id == id);
                    _categories.Remove(category);
                    return Results.Ok();
                });

                app.MapPut("/api/Categories",
                ([FromBody] Category item) =>
                {
                    var category = _categories.FirstOrDefault(x => x.Id == item.Id);
                    if (category != null) _categories.Remove(category);
                    _categories.Add(item);
                    return Results.Ok();
                });
            }
        }
    }
}
