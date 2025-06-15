using Coursework.Domain.Interfaces.Repositories;
using Coursework.Infrastructure;
using Coursework.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<CourseworkDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString(nameof(CourseworkDbContext)));
    }
    );

builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IFormRepository, FormRepository>();

builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();


app.Run();