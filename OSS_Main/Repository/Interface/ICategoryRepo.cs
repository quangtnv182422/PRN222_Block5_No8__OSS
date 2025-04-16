using OSS_Main.Models.Entity;

namespace OSS_Main.Repository.Interface
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
    }
}
