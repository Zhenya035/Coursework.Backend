using Coursework.Infrastructure;
using Coursework.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<CourseworkDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString(nameof(CourseworkDbContext)));
    }
    );

builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

app.Run();