using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Coursework.Application.Mapping;
using Coursework.Domain.Exceptions;
using Coursework.Domain.Interfaces.Repositories;

namespace Coursework.Application.Services;

public class AnswerService(
    IAnswerRepository repository,
    IFormRepository formRepository) : IAnswerService
{
    public async Task<List<GetAnswerDto>> GetAllByForm(uint formId)
    {
        if(!await formRepository.Exist(formId))
            throw new NotFoundException("Form");
        
        var answers = await repository.GetAllByForm(formId);
        return answers.Select(AnswerMapping.ToGetAnswerDto).ToList();
    }

    public async Task<GetAnswerDto> GetById(uint id)
    {
        await Exist(id);
        
        return AnswerMapping.ToGetAnswerDto(await repository.GetById(id));
    }

    private async Task Exist(uint id)
    {
        if(!await repository.Exist(id))
            throw new NotFoundException("Answer");
    }
}