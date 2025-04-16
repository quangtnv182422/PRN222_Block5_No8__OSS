using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;

namespace OSS_Main.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _categoryRepository;

        public CategoryService(ICategoryRepo categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByIdAsync(categoryId);
        }
    }
}
