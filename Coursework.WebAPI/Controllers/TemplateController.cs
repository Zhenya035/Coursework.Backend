using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[ApiController]
[Route("templates")]
public class TemplateController(ITemplateService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<GetTemplateDto>>> GetAll([FromBody]UserForTemplate user) =>
        Ok(await service.GetAll(user));
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetTemplateDto>> GetById(uint id) =>
        Ok(await service.GetById(id));

    [Authorize]
    [HttpPost("add/author/{authorId}")]
    public async Task<IActionResult> Create([FromBody] AddTemplateDto template, uint authorId)
    {
        await service.Create(template, authorId);
        return Ok();
    }

    [Authorize("AdminOnly")]
    [Authorize("OwnerOnly.Template")]
    [HttpPut("{templateId}/update")]
    public async Task<IActionResult> Update([FromBody] UpdateTemplateDto template, uint templateId)
    {
        await service.Update(template, templateId);
        return Ok();
    }

    [Authorize("AdminOnly")]
    [Authorize("OwnerOnly.Template")]
    [HttpDelete("{templateId}/delete")]
    public async Task<IActionResult> Delete(uint templateId)
    {
        await service.Delete(templateId);
        return Ok();
    }

    [Authorize("AdminOnly")]
    [Authorize("OwnerOnly.Template")]
    [HttpPut("{templateId}/addAuthorizedUsers")]
    public async Task<IActionResult> AddAuthorizedUsers([FromBody] AuthorizedUserDto users, uint templateId)
    {
        await service.AddAuthorizedUsers(users, templateId);
        return Ok();
    }
    
    [Authorize("AdminOnly")]
    [Authorize("OwnerOnly.Template")]
    [HttpPut("{templateId}/deleteAuthorizedUsers")]
    public async Task<IActionResult> DeleteAuthorizedUsers([FromBody] AuthorizedUserDto users, uint templateId)
    {
        await service.DeleteAuthorizedUsers(users, templateId);
        return Ok();
    }
}