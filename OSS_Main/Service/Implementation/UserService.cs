using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Models.Entity;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Interface;
using System.Security.Claims;

namespace OSS_Main.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepository;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<AspNetRole> _roleManager;
        public UserService(IUserRepo userRepository, UserManager<AspNetUser> userManager, IHttpContextAccessor httpContextAccessor, RoleManager<AspNetRole> roleManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<string> GetUserIdAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                string userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return userId;
            }
            return null;
        }

        public async Task<AspNetUser> GetCurrentUserAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            return await _userRepository.GetUserByIdAsync(userId);
        }



        public async Task<IEnumerable<AspNetUser>> GetUsersAsync(string searchQuery, string roleFilter, string statusFilter)
        {
            return await _userRepository.GetUsersAsync(searchQuery, roleFilter, statusFilter);
        }

        public async Task<List<string>> GetUserRolesAsync(AspNetUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<AspNetUser> GetUserByIdAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }


            // Lấy danh sách role của user
            var roles = await _userManager.GetRolesAsync(user);


            // 🔥 Kiểm tra nếu tài khoản bị khóa
            bool isLockedOut = user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow;

            return new AspNetUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Dob = user.Dob,
                Gender = user.Gender,
                LockoutEnabled = user.LockoutEnabled,
                Roles = roles.Select(roleName => new AspNetRole { Name = roleName }).ToList() // Gán roles vào user
            };
        }

        public async Task<AspNetUser> GetUserForUpdateByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }


        public async Task<bool> AddUserAsync(AspNetUser user, string password, string role)
        {
            // Tạo user bằng UserManager (chuẩn Identity)
            var createResult = await _userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                // Log lỗi để biết lỗi gì
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                Console.WriteLine(errors);
                return false;
            }
            // Kiểm tra nếu role không rỗng, thì gán role
            if (!string.IsNullOrEmpty(role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    var roleCreateResult = await _roleManager.CreateAsync(new AspNetRole { Name = role, NormalizedName = role.ToUpper() });
                    if (!roleCreateResult.Succeeded)
                    {
                        var errors = string.Join(", ", roleCreateResult.Errors.Select(e => e.Description));
                        Console.WriteLine(errors);
                        return false;
                    }
                }

                // Gán user vào role
                var addToRoleResult = await _userManager.AddToRoleAsync(user, role);
                if (!addToRoleResult.Succeeded)
                {
                    var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                    Console.WriteLine(errors);
                    return false;
                }
            }

            return true;
        }

        public async Task<List<string>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            return roles;
        }

        public async Task<bool> UpdateUserAsync(AspNetUser userInput)
        {
            var result = await _userManager.UpdateAsync(userInput);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserRolesAsync(AspNetUser user, string newRole)
        {
            // remove old role
            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                {
                    return false;
                }
            }

            //add new role
            if (!string.IsNullOrWhiteSpace(newRole))
            {
                var addResult = await _userManager.AddToRoleAsync(user, newRole);
                if (!addResult.Succeeded)
                {
                    return false;
                }
            }

            return true;
        }


        public async Task<bool> UpdateUserStatus(string userId, bool LockoutEnabled)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            user.LockoutEnabled = LockoutEnabled;
            return await UpdateUserAsync(user);
        }

        public async Task<string> AutoCreatePasswords()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            Random random = new Random();

            string password = new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return await Task.FromResult(password);
        }


        public async Task<AspNetUser> GetCurrentUserAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                return await _userManager.GetUserAsync(user);
            }
            return null;
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                return user.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return null;
        }
        public async Task<long> GetTotalUsersAsync() => await _userRepository.GetTotalUsersAsync();

        public async Task<List<AspNetUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

    }
}
