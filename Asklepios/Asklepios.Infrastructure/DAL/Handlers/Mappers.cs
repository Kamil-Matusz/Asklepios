using Asklepios.Core.DTO.Users;
using Asklepios.Core.Entities.Users;

namespace Asklepios.Infrastructure.DAL.Handlers;

public static class Mappers
{
    public static AccountDto AsAccountDto(this User entity)
        => new()
        {
            UserId = entity.UserId,
            Email = entity.Email,
            Role = entity.Role,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };
}