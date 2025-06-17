using Coursework.Application.Dto.Request.User;
using Coursework.Application.Dto.Response;
using Coursework.Application.Interfaces.Services;
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

    [HttpGet]
    public async Task<ActionResult<List<GetUserDto>>> GetAll() =>
        Ok(await service.GetAll());
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserDto>> GetById(uint id) =>
        Ok(await service.GetById(id));

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserNameDto user, uint id)
    {
        await service.Update(user, id);
        return Ok();
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> Delete(uint id)
    {
        await service.Delete(id);
        return Ok();
    }

    [HttpPut("{id}/block")]
    public async Task<IActionResult> Block(uint id)
    {
        await service.Block(id);
        return Ok();
    }

    [HttpPut("{id}/unBlock")]
    public async Task<IActionResult> UnBlock(uint id)
    {
        await service.UnBlock(id);
        return Ok();
    }

    [HttpPut("{id}/makeAdmin")]
    public async Task<IActionResult> MakeAdmin(uint id)
    {
        await service.MakeAdmin(id);
        return Ok();
    }
    
    [HttpPut("{id}/makeUser")]
    public async Task<IActionResult> MakeUser(uint id)
    {
        await service.MakeUser(id);
        return Ok();
    }
}