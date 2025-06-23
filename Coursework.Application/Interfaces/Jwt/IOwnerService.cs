namespace Coursework.Application.Interfaces.Jwt;

public interface IOwnerService<TEntity>
{
    public Task<uint?> GetOwnerId(uint id);
}