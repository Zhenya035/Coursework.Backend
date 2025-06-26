using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.WebAPI.Controllers;

[ApiController]
[Route("users")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<AuthorizationDto>> Register([FromBody] RegisterUserDto user) =>
        Ok(await service.Register(user));
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthorizationDto>> Login([FromBody] LoginUserDto user) =>
        Ok(await service.Login(user));

    [Authorize("AdminOnly")]
    [HttpGet("{id}/all")]
    public async Task<ActionResult<List<GetUserDto>>> GetAll(uint id) =>
        Ok(await service.GetAll(id));
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserDto>> GetById(uint id) =>
        Ok(await service.GetById(id));

    [Authorize]
    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserNameDto user, uint id)
    {
        await service.Update(user, id);
        return Ok();
    }

    [Authorize("AdminOnly")]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] List<uint> ids)
    {
        await service.Delete(ids);
        return Ok();
    }

    [Authorize("AdminOnly")]
    [HttpPut("block")]
    public async Task<IActionResult> Block([FromBody] List<uint> ids)
    {
        await service.Block(ids);
        return Ok();
    }

    [Authorize("AdminOnly")]
    [HttpPut("unBlock")]
    public async Task<IActionResult> UnBlock([FromBody] List<uint> ids)
    {
        await service.UnBlock(ids);
        return Ok();
    }

    [Authorize("AdminOnly")]
    [HttpPut("makeAdmin")]
    public async Task<IActionResult> MakeAdmin([FromBody] List<uint> ids)
    {
        await service.MakeAdmin(ids);
        return Ok();
    }
    
    [Authorize("AdminOnly")]
    [HttpPut("makeUser")]
    public async Task<IActionResult> MakeUser([FromBody] List<uint> ids)
    {
        await service.MakeUser(ids);
        return Ok();
    }
}