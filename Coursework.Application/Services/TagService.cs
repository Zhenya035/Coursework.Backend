using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;

namespace Coursework.Application.Services;

public class TagService(ITagRepository repository) : ITagService
{
    public async Task<List<GetTagDto>> GetAll()
    {
        var tags = await repository.GetAll();

        return tags.Select(TagMapping.ToGetTagDto).ToList();
    }

    public async Task<GetTagDto> GetById(uint id)
    {
        await Exist(id);
        
        return TagMapping.ToGetTagDto(await repository.GetById(id));
    }

    public async Task Add(AddTagDto tag)
    {
        if(tag is null)
            throw new InvalidInputDataException("Tag cannot be null");
        
        if(string.IsNullOrWhiteSpace(tag.Name))
            throw new InvalidInputDataException("Tag name cannot be empty");

        if(await Exist(tag.Name))
            throw new AlreadyAddedException("Tag");

        var newTag = TagMapping.FromAddTagDto(tag);
        
        await repository.Add(newTag);
    }

    public async Task Update(string name, uint id)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new InvalidInputDataException("Tag name cannot be empty");

        await Exist(id);
        
        await repository.Update(name, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    public async Task Exist(uint id)
    {
        if (!await repository.Exist(id))
            throw new NotFoundException("Tag");
    }

    private async Task<bool> Exist(string name) =>
        await repository.Exist(name);
}