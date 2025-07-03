using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

//[Authorize]
[ApiController]
[Route("likes")]
public class LikeController(ILikeService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetLikeDto>> GetById(uint id) =>
        Ok(await service.GetById(id));
    
    [HttpPost("add/template/{templateId}/author/{authorId}")]
    public async Task<IActionResult> Add(uint templateId, uint authorId)
    {
        await service.Add(templateId, authorId);
        return Ok();
    }

    [HttpDelete("delete/template/{templateId}/author/{authorId}")]
    public async Task<IActionResult> Delete(uint templateId, uint authorId)
    {
        await service.Delete(templateId, authorId);
        return Ok();
    }

    [HttpGet("check/author/{authorId}/template/{templateId}")]
    public async Task<ActionResult<bool>> Check(uint authorId, uint templateId) =>
        Ok(await service.Exist(authorId, templateId));
}