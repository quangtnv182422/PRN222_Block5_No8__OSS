using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using System.Diagnostics;

namespace OSS_Main.Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<AspNetUser> _userManager;

        public UserRepo(UserManager<AspNetUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AspNetUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<IEnumerable<AspNetUser>> GetUsersAsync(string searchQuery, string roleFilter, string statusFilter)
        {
            var query = _userManager.Users.AsQueryable();

            // Lọc theo từ khoá tìm kiếm
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(u => u.UserName.Contains(searchQuery) || u.Email.Contains(searchQuery));
            }

            // Lọc theo Status (Active/Deactivated)
            if (!string.IsNullOrEmpty(statusFilter))
            {
                bool isDeactivated = statusFilter == "Deactivated";
                query = query.Where(u => u.LockoutEnabled == isDeactivated);
            }

            var users = await query.ToListAsync();

            if (!string.IsNullOrEmpty(roleFilter))
            {
                // Lọc role theo filter
                var filteredUsers = new List<AspNetUser>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains(roleFilter))
                    {
                        filteredUsers.Add(user);
                    }
                }
                return filteredUsers;
            }
            else
            {
                //Lọc role khác Admin
                var notAdminUser = new List<AspNetUser>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (!roles.Contains("Admin"))
                    {
                        notAdminUser.Add(user);
                    }
                }

                return notAdminUser;
            }
        }

        public async Task<bool> AddUserAsync(AspNetUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                Debug.WriteLine($"[DEBUG] Failed to add user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserAsync(AspNetUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                Debug.WriteLine($"[DEBUG] Failed to add user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<long> GetTotalUsersAsync() => await _userManager.Users.LongCountAsync();
    }
}
