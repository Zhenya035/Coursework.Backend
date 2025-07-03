using Coursework.Application.Dto.Request.AddDtos;
using Coursework.Application.Dto.Request.UpdateDtos;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[Authorize]
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
    
    [Authorize("OwnerOrAdminOnly.Comment")]
    [HttpPut("{commentId}/update")]
    public async Task<IActionResult> Update([FromBody] UpdateCommentDto comment, uint commentId)
    {
        await service.Update(comment, commentId);
        return Ok();
    }

    [Authorize("OwnerOrAdminOnly.Comment")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] List<uint> ids)
    {
        await service.Delete(ids);
        return Ok();
    }
}