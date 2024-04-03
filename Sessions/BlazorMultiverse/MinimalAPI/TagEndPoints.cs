using Data.Models.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Server.Endpoints
{
    public static class TagEndPoints
    {
        private static List<Tag> _tags = new()
        {
            new Tag() { Id = Guid.NewGuid().ToString(), Name = "Web Development" },
            new Tag() { Id = Guid.NewGuid().ToString(), Name = "Azure"}
        };
        public static void MapTagEndpoints(this WebApplication app)
        {
            app.MapGet("/api/Tags",
            () =>
            {
                return Results.Ok(_tags);
            });
            
            app.MapGet("/api/Tags/{*id}",
            (string id) =>
            {
                return Results.Ok(_tags.FirstOrDefault(x => x.Id == id));
            });
            
            app.MapDelete("/api/Tags/{*id}",
            (string id) =>
            {
                var tag = _tags.FirstOrDefault(x => x.Id == id);
                _tags.Remove(tag);
                return Results.Ok();
            });

            app.MapPut("/api/Tags",
            ([FromBody] Tag item) =>
            {
                var tag = _tags.FirstOrDefault(x => x.Id == item.Id);
                if (tag != null) _tags.Remove(tag);
                _tags.Add(item);
                return Results.Ok(item);
            });
        }
    }
}
