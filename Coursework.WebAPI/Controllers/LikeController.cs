using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

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
        await service.Add(authorId, templateId);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }

    [HttpGet("{id}/check")]//todo переделать
    public async Task<IActionResult> Check(uint id)
    {
        await service.Exist(id);
        return Ok();
    }
}