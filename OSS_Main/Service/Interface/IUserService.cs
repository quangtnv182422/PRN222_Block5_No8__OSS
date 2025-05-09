using OSS_Main.Models.Entity;

namespace OSS_Main.Service.Interface
{
    public interface IUserService
    {
        Task<string> GetUserIdAsync(HttpContext httpContext);
        Task<AspNetUser> GetCurrentUserAsync(string userId);
        Task<IEnumerable<AspNetUser>> GetUsersAsync(string searchQuery, string roleFilter, string statusFilter);
        Task<List<string>> GetUserRolesAsync(AspNetUser user);
        Task<AspNetUser> GetUserByIdAsync(string userId);
        Task<AspNetUser> GetUserForUpdateByIdAsync(string userId);
        Task<bool> AddUserAsync(AspNetUser user, string password, string role);
        Task<List<string>> GetAllRolesAsync();
        Task<bool> UpdateUserAsync(AspNetUser userInput);
        Task<bool> UpdateUserRolesAsync(AspNetUser user, string newRole);
        Task<bool> UpdateUserStatus(string userId, bool status);
        Task<string> AutoCreatePasswords();
        Task<AspNetUser> GetCurrentUserAsync();
        Task<string> GetCurrentUserIdAsync();
        Task<long> GetTotalUsersAsync();
        Task<List<AspNetUser>> GetAllUsersAsync();
    }
}
