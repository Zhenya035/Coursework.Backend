using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("forms")]
public class FormController(IFormService service) : ControllerBase
{
    [Authorize("OwnerOrAdminOnly.Template")]
    [HttpGet("template/{templateId}")]
    public async Task<ActionResult<List<GetFormDto>>> GetAllTemplate(uint templateId) =>
        Ok(await service.GetAllByTemplate(templateId));
    
    [Authorize("OwnerOrAdminOnly.Form")]
    [HttpGet("{formId}")]
    public async Task<ActionResult<GetFormDto>> GetById(uint formId) =>
        Ok(await service.GetById(formId));

    [HttpPost("add/template/{templateId}/author/{authorId}")]
    public async Task<IActionResult> Fill([FromBody] AddOrUpdateFormDto form, uint templateId, uint authorId)
    {
        await service.Fill(form, templateId, authorId);
        return Ok();
    }

    [Authorize("OwnerOrAdminOnly.Form")]
    [HttpPut("{formId}/update")]
    public async Task<IActionResult> Edit([FromBody] AddOrUpdateFormDto form, uint formId)
    {
        await service.Edit(form, formId);
        return Ok();
    }

    [Authorize("OwnerOrAdminOnly.Form")]    
    [HttpDelete("{formId}/delete")]
    public async Task<IActionResult> Delete(uint formId)
    {
        await service.Delete(formId);
        return Ok();
    }
}