using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[ApiController]
[Route("templates")]
public class TemplateController(ITemplateService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<GetTemplateDto>>> GetAll([FromBody]UserForTemplate user) =>
        Ok(await service.GetAll(user));
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetTemplateDto>> GetById(uint id) =>
        Ok(await service.GetById(id));

    [HttpPost("add/author/{authorId}")]
    public async Task<IActionResult> Create([FromBody] AddTemplateDto template, uint authorId)
    {
        await service.Create(template, authorId);
        return Ok();
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update([FromBody] UpdateTemplateDto template, uint id)
    {
        await service.Update(template, id);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }

    [HttpPut("{id}/addAuthorizedUsers")]
    public async Task<IActionResult> AddAuthorizedUsers([FromBody] AuthorizedUserDto users, uint id)
    {
        await service.AddAuthorizedUsers(users, id);
        return Ok();
    }
    
    [HttpPut("{id}/deleteAuthorizedUsers")]
    public async Task<IActionResult> DeleteAuthorizedUsers([FromBody] AuthorizedUserDto users, uint id)
    {
        await service.DeleteAuthorizedUsers(users, id);
        return Ok();
    }
}