using Microsoft.EntityFrameworkCore;
using ECommerce.Server.DAL;
using ECommerce.Server.Models.DataModels;
using ECommerce.Server.Repositories.Interfaces;

namespace ECommerce.Server.Repositories
{
    public class CategoryRepository(POSDbContext dbContext) : ICategoryRepository
    {
        private readonly POSDbContext _dbContext = dbContext;

        public async Task<List<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategory(int categoryId)
        {
            return await _dbContext.Categories.FindAsync(categoryId);
        }

        public async Task AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategory(int categoryId)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
