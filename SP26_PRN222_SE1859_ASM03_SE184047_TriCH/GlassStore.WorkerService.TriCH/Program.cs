using GlassStore.Repositories.TriCH;
using GlassStore.Repositories.TriCH.DBContext;
using GlassStore.Services.TriCH;
using GlassStore.WorkerService.TriCH;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Vu111";
});

builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<PRN222_EYEWEARSHOPContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductTriCHRepository>();

builder.Services.AddScoped<CategoryTriCHRepository>();

builder.Services.AddScoped<IProductTriCHService, ProductTriCHService>();
builder.Services.AddScoped<ICategoryTriCHService, CategoryTriCHService>();

var host = builder.Build();
host.Run();