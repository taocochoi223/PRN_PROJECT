using Data.Models;
using Microsoft.EntityFrameworkCore;
using Q2_BookManagement.Business.Service;
using Q2_BookManagement.Data.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PePrn25fallB523Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<BookRepo>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "book_details",
    pattern: "Book/{id}",
    defaults: new { controller = "Books", action = "Details" });

app.MapControllerRoute(
    name: "book_list",
    pattern: "Book",
    defaults: new { controller = "Books", action = "Index" });

app.Run();
