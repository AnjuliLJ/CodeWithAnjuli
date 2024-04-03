﻿using BlazorWebAssembly.Server.Endpoints;
using Data;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>()
    .Configure(options =>
    {
        options.DataPath = @"..\..\..\..\Data\";
        options.BlogPostsFolder = "Blogposts";
        options.TagsFolder = "Tags";
        options.CategoriesFolder = "Categories";
    });
builder.Services.AddScoped<IBlogApi, BlogApiDummy>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapBlogPostApi();
app.MapCategoryApi();
app.MapTagApi();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

