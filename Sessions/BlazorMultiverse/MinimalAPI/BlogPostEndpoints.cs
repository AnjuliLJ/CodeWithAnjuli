using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssembly.Server.Endpoints
{
    public static class BlogPostEndpoints
    {
        private static List<BlogPost> _blogPosts = new()
        {
            new BlogPost() {
                Id = Guid.NewGuid().ToString(),
                Title = "Why Blazor is amazing",
                PublishDate = DateTime.Now,
                Category = new Category() { Id = Guid.NewGuid().ToString(), Name = "Blazor"},
                Tags = new List<Tag> { new Tag() { Id = Guid.NewGuid().ToString(), Name = "Web Development" } }
            }
        };

        public static void MapBlogPostEndpoints(this WebApplication app)
        {
            app.MapGet("/api/BlogPosts",
                () =>
                {
                    return Results.Ok(_blogPosts);
                });

            app.MapGet("/api/BlogPostCount", () =>
            {
                return Results.Ok(_blogPosts.Count);
            });

            app.MapGet("/api/BlogPosts/{*id}", (string id) =>
            {
                return Results.Ok(_blogPosts.FirstOrDefault(x => x.Id == id));
            });

            app.MapPut("/api/BlogPosts", ([FromBody] BlogPost item) =>
            {
                var blogPost = _blogPosts.FirstOrDefault(x => x.Id == item.Id);
                if (blogPost is not null) _blogPosts.Remove(blogPost);
                _blogPosts.Add(item);
                return Results.Ok(item);
            });

            app.MapDelete("/api/BlogPosts/{*id}", (string id) =>
            {
                var blogPost = _blogPosts.FirstOrDefault(y => y.Id == id);
                if (blogPost != null) { _blogPosts.Remove(blogPost); }
                return Results.Ok();
            });
        }
    }
}
