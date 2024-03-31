using Data.Models;
using Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data
{
    public class BlogApiDummy : IBlogApi
    {
        private List<BlogPost> posts = new();
        private List<Category> categories = new();
        private List<Tag> tags = new();

        public BlogApiDummy()
        {
            var category = new Category() { Id = Guid.NewGuid().ToString(), Name = "Blazor" };
            var category2 = new Category() { Id = Guid.NewGuid().ToString(), Name = ".NET" };
            categories.Add(category);
            categories.Add(category2);
            var tag = new Tag() { Id = Guid.NewGuid().ToString(), Name = "Web Development" };
            var tag2 = new Tag() { Id = Guid.NewGuid().ToString(), Name = "Azure" };
            tags.Add(tag);
            tags.Add(tag2);
            var post = new BlogPost()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Why Blazor is amazing",
                PublishDate = DateTime.Now,
                Category = category,
                Tags = new List<Tag> { tag }
            };
            posts.Add(post);

        }
        public Task DeleteBlogPostAsync(string id)
        {
            var post = posts.FirstOrDefault(x => x.Id == id);
            if (post != null) posts.Remove(post);
            return Task.CompletedTask;
        }

        public Task DeleteCategoryAsync(string id)
        {
            var category = categories.FirstOrDefault(x => x.Id == id);
            if (category != null) categories.Remove(category);
            return Task.CompletedTask;
        }

        public Task DeleteTagAsync(string id)
        {
            var tag = tags.FirstOrDefault(x => x.Id == id);
            if (tag != null) tags.Remove(tag);
            return Task.CompletedTask;
        }

        public Task<BlogPost?> GetBlogPostAsync(string id)
        {
            return Task.FromResult(posts.FirstOrDefault(x => x.Id == id));
        }

        public Task<int> GetBlogPostCountAsync()
        {
            return Task.FromResult(posts.Count());
        }

        public Task<List<BlogPost>?> GetBlogPostsAsync(int numberOfPosts, int startIndex)
        {
            return Task.FromResult(posts);
        }

        public Task<List<Category>?> GetCategoriesAsync()
        {
            return Task.FromResult(categories);
        }

        public Task<Category?> GetCategoryAsync(string id)
        {
           return Task.FromResult(categories.FirstOrDefault(x => x.Id == id));
        }

        public Task<Tag?> GetTagAsync(string id)
        {
            return Task.FromResult(tags.FirstOrDefault(x => x.Id == id));
        }

        public Task<List<Tag>?> GetTagsAsync()
        {
            return Task.FromResult(tags);
        }

        public Task InvalidateCacheAsync()
        {
            posts = null;
            categories = null;
            tags = null;
            return Task.CompletedTask;
        }

        public Task<BlogPost?> SaveBlogPostAsync(BlogPost item)
        {
            posts.Add(item);
            return Task.FromResult(item);
        }

        public Task<Category?> SaveCategoryAsync(Category item)
        {
            categories.Add(item);
            return Task.FromResult(item);
        }

        public Task<Tag?> SaveTagAsync(Tag item)
        {
            tags.Add(item);
            return Task.FromResult(item);
        }
    }
}
