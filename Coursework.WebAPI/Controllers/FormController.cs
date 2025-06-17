using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[ApiController]
[Route("forms")]
public class FormController(IFormService service) : ControllerBase
{
    [HttpGet("template/{templateId}")]
    public async Task<ActionResult<List<GetFormDto>>> GetAllTemplate(uint templateId) =>
        Ok(await service.GetAllByTemplate(templateId));
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetFormDto>> GetById(uint id) =>
        Ok(await service.GetById(id));

    [HttpPost("add/template/{templateId}/author/{authorId}")] //todo продумать создание ответов
    public async Task<IActionResult> Fill([FromBody] AddOrUpdateFormDto form, uint templateId, uint authorId)
    {
        await service.Fill(form, templateId, authorId);
        return Ok();
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Edit([FromBody] AddOrUpdateFormDto form, uint id)
    {
        await service.Edit(form, id);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }
}