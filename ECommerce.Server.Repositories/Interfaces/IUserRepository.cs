using ECommerce.Server.Models.DataModels;

namespace ECommerce.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<POSUser?> GetUserByUsernameAsync(string username);
    }
}
