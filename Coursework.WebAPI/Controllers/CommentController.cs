using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[ApiController]
[Route("comments")]
public class CommentController(ICommentService service) : ControllerBase
{
    [HttpGet("template/{templateId}")]
    public async Task<ActionResult<List<GetCommentDto>>> GetAllByTemplate(uint templateId) =>
        Ok(await service.GetAllByTemplate(templateId));
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCommentDto>> GetById(uint id) => 
        Ok(await service.GetById(id));

    [HttpPost("add/template/{templateId}/author/{authorId}")]
    public async Task<IActionResult> Add([FromBody] AddCommentDto comment, uint templateId, uint authorId)
    {
        await service.Add(comment, templateId, authorId);
        return Ok();
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update([FromBody] UpdateCommentDto comment, uint id)
    {
        await service.Update(comment, id);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }
}