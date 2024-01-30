using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ECommerce.Server.DAL;
using ECommerce.Server.Repositories.Interfaces;

namespace ECommerce.Server.Repositories
{
    public class UserRolesRepository(POSDbContext dbContext) : IUserRolesRepository
    {
        private readonly POSDbContext _dbContext = dbContext;

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<IdentityRole?> GetRole(int roleId)
        {
            return await _dbContext.Roles.FindAsync(roleId);
        }

        public async Task AddRole(IdentityRole role)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRole(IdentityRole role)
        {
            _dbContext.Entry(role).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRole(int roleId)
        {
            var role = await _dbContext.Roles.FindAsync(roleId);
            if (role != null)
            {
                _dbContext.Roles.Remove(role);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
