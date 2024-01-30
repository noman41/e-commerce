using Microsoft.EntityFrameworkCore;
using ECommerce.Server.DAL;
using ECommerce.Server.Models.DataModels;
using ECommerce.Server.Repositories.Interfaces;

namespace ECommerce.Server.Repositories
{
    public class UserRepository(POSDbContext dbContext) : IUserRepository
    {
        private readonly POSDbContext _dbContext = dbContext;

        public async Task<POSUser?> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
