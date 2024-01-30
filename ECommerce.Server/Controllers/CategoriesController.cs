using Microsoft.AspNetCore.Mvc;
using ECommerce.Server.Filters;
using ECommerce.Server.Models.DataModels;
using ECommerce.Server.Repositories.Interfaces;

namespace ECommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PoSExceptionFilter]
    public class CategoriesController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryRepository.GetCategories());
        }

        [HttpGet]
        [Route("GetCategory")]
        public async Task<IActionResult> GetCategory([FromQuery] int categoryId)
        {
            var category = await _categoryRepository.GetCategory(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _categoryRepository.AddCategory(category);
            return Ok();
        }

        [HttpPost]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] Category category)
        {
            if (categoryId != category.CategoryId)
            {
                return BadRequest();
            }
            await _categoryRepository.UpdateCategory(category);
            return Ok();
        }

        [HttpPost]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _categoryRepository.DeleteCategory(categoryId);
            return Ok();
        }
    }
}
