using OSS_Main.Models.Entity;

namespace OSS_Main.Service.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
    }
}
