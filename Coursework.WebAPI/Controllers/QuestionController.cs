using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("questions")]
public class QuestionController(IQuestionService service) : ControllerBase
{
    [HttpGet("template/{templateId}")]
    public async Task<ActionResult<List<GetQuestionDto>>> GetAllByTemplate(uint templateId) =>
        Ok(await service.GetAllByTemplate(templateId));
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetQuestionDto>> GetById(uint id) =>
        Ok(await service.GetById(id));

    [HttpPost("add/template/{templateId}")]
    public async Task<IActionResult> Add(AddQuestionDto question, uint templateId)
    {
        await service.Add(question, templateId);
        return Ok();
    }
    
    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update(UpdateQuestionDto question, uint id)
    {
        await service.Update(question, id);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }

    [HttpPut("{id}/display")]
    public async Task<IActionResult> MakeDisplay(uint id)
    {
        await service.MakeDisplay(id);
        return Ok();
    }

    [HttpPut("{id}/notDisplay")]
    public async Task<IActionResult> MakeNotDisplay(uint id)
    {
        await service.MakeNotDisplay(id);
        return Ok();
    }
}