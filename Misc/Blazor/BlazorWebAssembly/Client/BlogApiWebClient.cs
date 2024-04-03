﻿using Data.Models;
using Data.Models.Interfaces;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Text.Json;

namespace BlazorWebAssembly.Client
{
    public class BlogApiWebClient : IBlogApi
    {
        private readonly IHttpClientFactory _factory;
        public BlogApiWebClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<BlogPost?> GetBlogPostAsync(string id)
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<BlogPost>($"api/BlogPosts/{id}");
        }
        public async Task<int> GetBlogPostCountAsync()
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<int>("/api/BlogPostCount");
        }
        public async Task<List<BlogPost>?> GetBlogPostsAsync(int numberofposts, int startindex)
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<List<BlogPost>>($"/api/BlogPosts?numberofposts={numberofposts}&startindex={startindex}");
        }
        public async Task<BlogPost?> SaveBlogPostAsync(BlogPost item)
        {
            try
            {
                var httpclient = _factory.CreateClient("Authenticated");
                var response = await httpclient.PutAsJsonAsync<BlogPost>
                   ("api/BlogPosts", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<BlogPost>(json);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return null;
        }
        public async Task DeleteBlogPostAsync(string id)
        {
            try
            {
                var httpclient = _factory.CreateClient("Authenticated");
                await httpclient.DeleteAsync($"api/BlogPosts/{id}");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
        public async Task<List<Category>?> GetCategoriesAsync()
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<List<Category>>($"api/Categories");
        }
        public async Task<Category?> GetCategoryAsync(string id)
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<Category>($"api/Categories/{id}");
        }
        public async Task DeleteCategoryAsync(string id)
        {
            try
            {
                var httpclient = _factory.CreateClient("Authenticated");
                await httpclient.DeleteAsync($"api/Categories/{id}");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
        public async Task<Category?> SaveCategoryAsync(Category item)
        {
            try
            {
                var httpclient = _factory.CreateClient("Authenticated");
                var response = await httpclient.PutAsJsonAsync<Category>("api/Categories", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Category>(json);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return null;
        }
        public async Task<Tag?> GetTagAsync(string id)
        {
            var httpclient = _factory.CreateClient("Public");
            return await httpclient.GetFromJsonAsync<Tag>($"api/Tags/{id}");
        }
        public async Task<List<Tag>?> GetTagsAsync()
        {
            var httpclient = _factory.CreateClient("Public");

            return await httpclient.GetFromJsonAsync<List<Tag>>($"api/Tags");
        }
        public async Task DeleteTagAsync(string id)
        {
            try
            {
                var httpclient = _factory.CreateClient("Authenticated");
                await httpclient.DeleteAsync($"api/Tags/{id}");
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
        }
        public async Task<Tag?> SaveTagAsync(Tag item)
        {
            try
            {
                var httpclient = _factory.CreateClient("Authenticated");
                var response = await httpclient.PutAsJsonAsync<Tag>("api/Tags", item);
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Tag>(json);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            return null;
        }
        public Task InvalidateCacheAsync()
        {
            throw new NotImplementedException();
        }
    }
}
