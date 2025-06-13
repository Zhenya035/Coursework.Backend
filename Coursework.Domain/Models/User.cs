using Coursework.Domain.Enums;

namespace Coursework.Domain.Models;

public class User
{
    public uint Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public RoleEnum Role { get; set; } = RoleEnum.User;
    public List<Comment> Comments { get; set; } = [];
    public List<Template> Templates { get; set; } = [];
    public List<Form> Forms { get; set; } = [];
    public List<Like> Likes { get; set; } = [];
}