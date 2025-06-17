using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[ApiController]
[Route("answers")]
public class AnswerController(IAnswerService service) : ControllerBase
{
    [HttpGet("form/{formId}")]
    public async Task<ActionResult<List<GetAnswerDto>>> GetAllByForm(uint formId) =>
        Ok(await service.GetAllByForm(formId));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAnswerDto>> GetById(uint id) =>
        Ok(await service.GetById(id));

    [HttpPost("add/question/{questionId}")]
    public async Task<IActionResult> Add([FromBody] AddAnswerDto answer, uint questionId)
    {
        await service.Create(answer, questionId);
        return Ok();
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update([FromBody] UpdateAnswerDto newValue, uint id)
    {
        await service.Update(newValue, id);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }
}