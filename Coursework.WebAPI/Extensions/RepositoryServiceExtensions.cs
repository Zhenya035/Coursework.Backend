using Coursework.Domain.Interfaces.Repositories;
using Coursework.Infrastructure.Repositories;

namespace Coursework.WebAPI.Extensions;

public static class RepositoryServiceExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAnswerRepository, AnswerRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IFormRepository, FormRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}