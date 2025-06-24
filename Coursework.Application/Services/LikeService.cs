using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Jwt;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;
using Coursework.Domain.Models;

namespace Coursework.Application.Services;

public class LikeService(ILikeRepository repository) : ILikeService, IOwnerService<Like>
{
    public async Task<GetLikeDto> GetById(uint id)
    {
        await Exist(id);
        
        return LikeMapping.ToGetLikeDto(await repository.GetById(id));
    }

    public async Task Add(uint authorId, uint templateId)
    {
        if(await Exist(authorId, templateId))
            throw new AlreadyAddedException("Like");
        
        var like = new Like()
        {
            AuthorId = authorId,
            TemplateId = templateId,
        };
        await repository.Add(like);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Like");
    }
    
    private async Task<bool> Exist(uint authorId, uint templateId) =>
        await repository.Exist(authorId, templateId);

    public async Task<uint?> GetOwnerId(uint id)
    {
        await Exist(id);

        var like = await repository.GetById(id);
        return like.AuthorId;
    }
}