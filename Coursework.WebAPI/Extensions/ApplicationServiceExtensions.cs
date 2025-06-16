using Coursework.Application.Interfaces.Services;
using Coursework.Application.Services;

namespace Coursework.WebAPI.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAnswerService, AnswerService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IFormService, FormService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IUserService, UserService>();
    }
}