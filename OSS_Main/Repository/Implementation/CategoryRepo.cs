using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;

namespace OSS_Main.Repository.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {

        private readonly Prn222ProjectContext _context;
        public CategoryRepo(Prn222ProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

    }
}
