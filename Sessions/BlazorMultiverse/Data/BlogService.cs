using Data.Models;
using Data.Models.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
namespace Data;

public class BlogService : IBlogService
{
    private string _baseUri = "https://localhost:7139/api";
    public async Task DeleteBlogPostAsync(string id)
    {
        var http = new HttpClient();
        await http.DeleteAsync($"{_baseUri}/BlogPosts/{id}"); 
    }

    public async Task DeleteCategoryAsync(string id)
    {
        var http = new HttpClient();
        await http.DeleteAsync($"{_baseUri}/Categories/{id}");
    }

    public async Task DeleteTagAsync(string id)
    {
        var http = new HttpClient();
        await http.DeleteAsync($"{_baseUri}/Tags/{id}");
    }

    public async Task<BlogPost?> GetBlogPostAsync(string id)
    {
        var http = new HttpClient();
        return await http.GetFromJsonAsync<BlogPost>($"{_baseUri}/BlogPosts/{id}");
    }

    public async Task<int> GetBlogPostCountAsync()
    {
        var http = new HttpClient();
        return await http.GetFromJsonAsync<int>($"{_baseUri}/BlogPostsCount");
    }

    public async Task<List<BlogPost>?> GetBlogPostsAsync(int numberOfPosts, int startIndex)
    {
        var http = new HttpClient();
        return await http.GetFromJsonAsync<List<BlogPost>>($"{_baseUri}/BlogPosts");

    }

    public async Task<List<Category>?> GetCategoriesAsync()
    {
        var http = new HttpClient();
        return await http.GetFromJsonAsync<List<Category>>($"{_baseUri}/Categories");
    }

    public async Task<Category?> GetCategoryAsync(string id)
    {
        var http = new HttpClient();
        return await http.GetFromJsonAsync<Category>($"{_baseUri}/Categories/{id}");
    }

    public async Task<Tag?> GetTagAsync(string id)
    {
        var http = new HttpClient();
        return await http.GetFromJsonAsync<Tag>($"{_baseUri}/Tags/{id}");
    }

    public async Task<List<Tag>?> GetTagsAsync()
    {
        var http = new HttpClient();
        return await http.GetFromJsonAsync<List<Tag>>($"{_baseUri}/Tags");
    }

    public async Task<BlogPost?> SaveBlogPostAsync(BlogPost item)
    {
        var http = new HttpClient();
        var response = await http.PutAsJsonAsync<BlogPost>($"{_baseUri}/BlogPosts", item);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<BlogPost>(json);
    }

    public async Task<Category?> SaveCategoryAsync(Category item)
    {
        var http = new HttpClient();
        var response = await http.PutAsJsonAsync<Category>($"{_baseUri}/Categories", item);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Category>(json);
    }

    public async Task<Tag?> SaveTagAsync(Tag item)
    {
        var http = new HttpClient();
        var response = await http.PutAsJsonAsync<Tag>($"{_baseUri}/Tags", item);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Tag>(json);
    }
}
