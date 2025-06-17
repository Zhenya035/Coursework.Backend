using Coursework.Application.Dto.Request;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[ApiController]
[Route("tags")]
public class TagController(ITagService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<GetTagDto>>> GetAllTags() =>
        Ok(await service.GetAll());
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetTagDto>> GetTag(uint id) =>
        Ok(await service.GetById(id));

    [HttpPost("add")]
    public async Task<IActionResult> Add(AddOrUpdateTagDto tag)
    {
        await service.Add(tag);
        return Ok();
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update(AddOrUpdateTagDto tag, uint id)
    {
        await service.Update(tag, id);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }
}