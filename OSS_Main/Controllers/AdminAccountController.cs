﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OSS_Main.Hubs;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Interface;

namespace OSS_Main.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminAccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IHubContext<AdminHub> _adminHubContext;
        public AdminAccountController(IUserService userService, UserManager<AspNetUser> userManager, IEmailService emailService, IHubContext<AdminHub> adminHubContext)
        {
            _userService = userService;
            _userManager = userManager;
            _emailService = emailService;
            _adminHubContext = adminHubContext;
        }

        public async Task<IActionResult> AccountList(string searchQuery, string roleFilter, string statusFilter, int page = 1, int pageSize = 10)
        {
            var users = await _userService.GetUsersAsync(searchQuery, roleFilter, statusFilter);

            int totalUsers = users.Count();
            var pagedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Lấy danh sách Roles cho từng User
            var userRoles = new Dictionary<string, List<string>>();
            foreach (var user in pagedUsers)
            {
                var roles = await _userService.GetUserRolesAsync(user);
                userRoles[user.Id] = roles.ToList();
            }

            // Truyền roles vào ViewBag
            ViewBag.UserRoles = userRoles;

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalUsers / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.RoleFilter = roleFilter;
            ViewBag.StatusFilter = statusFilter;

            return View(pagedUsers);
        }
        // Xem chi tiết user
        public async Task<IActionResult> AccountDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new { message = "User ID is required." });
            }

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Lấy danh sách role của user
            var roles = await _userService.GetUserRolesAsync(user);
            ViewBag.UserRoles = roles; // Truyền role của user vào ViewBag

            // Lấy tất cả roles có trong hệ thống
            var allRoles = await _userService.GetAllRolesAsync();
            ViewBag.AllRoles = allRoles; // Truyền tất cả roles vào ViewBag

            return View("AccountDetail", user);
        }

        // Thêm user mới
        [HttpPost]
        public async Task<IActionResult> AddUser(AspNetUser userInput, string Role)
        {
            if (string.IsNullOrWhiteSpace(userInput.UserName) || string.IsNullOrWhiteSpace(userInput.Email))
            {
                TempData["Error"] = "Username, Email are required. Try again.";
                return RedirectToAction("AccountList");
            }

            var existingUser = await _userManager.FindByEmailAsync(userInput.Email);
            if (existingUser != null)
            {
                TempData["Error"] = "User with this email already exists. Try again.";
                return RedirectToAction("AccountList");
            }

            string password = await _userService.AutoCreatePasswords();

            var user = new AspNetUser
            {
                UserName = userInput.UserName,
                Email = userInput.Email,
                PhoneNumber = userInput.PhoneNumber,
                Dob = userInput.Dob,
                Gender = userInput.Gender,
            };
            // Cập nhật email đã được xác nhận ngay lập tức
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _userManager.ConfirmEmailAsync(user, confirmationToken);
            user.LockoutEnabled = false;

            bool result = await _userService.AddUserAsync(user, password, Role);

            if (result)
            {
                await _emailService.SendWelcomeEmail(userInput.Email, userInput.UserName, password);
                await _adminHubContext.Clients.All.SendAsync("UpdateAccount");
                return RedirectToAction("AccountList");
            }
            else
            {
                TempData["Error"] = "Failed to add user.";
                return RedirectToAction("AccountList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(string Id, bool LockoutEnabled)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return Json(new { success = false, message = "User ID is required." });
            }

            bool success = await _userService.UpdateUserStatus(Id, LockoutEnabled);
            if (success)
            {
                await _adminHubContext.Clients.All.SendAsync("UpdateAccount");
                return RedirectToAction("AccountList");
            }
            else
            {
                TempData["Error"] = "User may not exist.";
                return RedirectToAction("AccountList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(AspNetUser userInput, string Role)
        {
            var existingUser = await _userService.GetUserForUpdateByIdAsync(userInput.Id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            existingUser.UserName = userInput.UserName;
            existingUser.Email = userInput.Email;
            existingUser.PhoneNumber = userInput.PhoneNumber;
            existingUser.Dob = userInput.Dob;
            existingUser.Gender = userInput.Gender;
            existingUser.LockoutEnabled = userInput.LockoutEnabled;

            // Gọi hàm update user
            var isUserUpdated = await _userService.UpdateUserAsync(existingUser);

            // Cập nhật role dựa trên "existingUser" (cùng 1 instance)
            var isRolesUpdated = await _userService.UpdateUserRolesAsync(existingUser, Role);
            if (!isRolesUpdated)
                return BadRequest("Failed to update user roles.");

            if (isUserUpdated)
            {
                await _adminHubContext.Clients.All.SendAsync("UpdateAccount");
                return RedirectToAction("AccountDetail", new { id = existingUser.Id });
            }
            else
            {
                TempData["Error"] = "Failed to update user.";
                return RedirectToAction("AccountDetail", new { id = existingUser.Id });
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
