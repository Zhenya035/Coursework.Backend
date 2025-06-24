using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("answers")]
public class AnswerController(IAnswerService service) : ControllerBase
{
    [HttpGet("form/{formId}")]
    public async Task<ActionResult<List<GetAnswerDto>>> GetAllByForm(uint formId) =>
        Ok(await service.GetAllByForm(formId));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAnswerDto>> GetById(uint id) =>
        Ok(await service.GetById(id));
}