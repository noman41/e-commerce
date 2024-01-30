using Microsoft.AspNetCore.Identity;

namespace ECommerce.Server.Repositories.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<List<IdentityRole>> GetRoles();
        Task<IdentityRole?> GetRole(int roleId);
        Task AddRole(IdentityRole role);
        Task UpdateRole(IdentityRole role);
        Task DeleteRole(int roleId);
    }
}
