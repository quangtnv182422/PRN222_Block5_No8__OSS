using OSS_Main.Models.Entity;

namespace OSS_Main.Repository.Interface
{
    public interface IUserRepo
    {
        Task<AspNetUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<AspNetUser>> GetUsersAsync(string searchQuery, string roleFilter, string statusFilter);
        Task<bool> AddUserAsync(AspNetUser user, string password);
        Task<bool> UpdateUserAsync(AspNetUser user);
        Task<bool> DeleteUserAsync(string userId);
        Task<long> GetTotalUsersAsync();
    }
}
