using GlassStore.Repositories.TriCH.DBContext;
using Microsoft.EntityFrameworkCore;
using GlassStore.Repositories.TriCH;
using GlassStore.Services.TriCH;
using GlassStore.Razor.WebAppTriCH.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Forbidden";
    });

// Register DbContext
builder.Services.AddDbContext<PRN222_EYEWEARSHOPContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped<ProductTriCHRepository>();
builder.Services.AddScoped<CategoryTriCHRepository>();

builder.Services.AddScoped<UserAccountRepository>();

// Register Services
builder.Services.AddScoped<IProductTriCHService, ProductTriCHService>();
builder.Services.AddScoped<ICategoryTriCHService, CategoryTriCHService>();

builder.Services.AddScoped<UserAccoutService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<EyewareHub>("/EyewareHub");
app.MapRazorPages();

app.Run();
