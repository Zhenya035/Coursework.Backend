using Coursework.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<CourseworkDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString(nameof(CourseworkDbContext)));
    }
    );

var app = builder.Build();


app.Run();