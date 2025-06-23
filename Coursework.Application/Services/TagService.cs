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

    public async Task Add(AddOrUpdateTagDto newTagDto)
    {
        if(newTagDto is null)
            throw new InvalidInputDataException("Tag cannot be null");
        
        if(string.IsNullOrWhiteSpace(newTagDto.Name))
            throw new InvalidInputDataException("Tag name cannot be empty");

        if(await Exist(newTagDto.Name))
            throw new AlreadyAddedException("Tag");

        var newTag = TagMapping.FromAddTagDto(newTagDto);
        
        await repository.Add(newTag);
    }

    public async Task Update(AddOrUpdateTagDto newTag, uint id)
    {
        if(string.IsNullOrWhiteSpace(newTag.Name))
            throw new InvalidInputDataException("Tag name cannot be empty");
        
        if(await Exist(newTag.Name))
            throw new AlreadyAddedException("Tag with this name");

        await Exist(id);
        
        await repository.Update(newTag.Name, id);
    }

    public async Task Delete(uint id)
    {
        await Exist(id);
        
        await repository.Delete(id);
    }

    private async Task Exist(uint id)
    {
        if (!await repository.Exist(id))
            throw new NotFoundException("Tag");
    }

    private async Task<bool> Exist(string name) =>
        await repository.Exist(name);
}