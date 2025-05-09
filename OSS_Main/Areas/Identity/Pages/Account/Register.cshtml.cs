﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OSS_Main.Models.Entity;
using OSS_Main.Service.Interface;

namespace OSS_Main.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IUserStore<AspNetUser> _userStore;
        private readonly IUserEmailStore<AspNetUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICartService _cartService;
        private readonly RoleManager<AspNetRole> _roleManager;

        public RegisterModel(
            UserManager<AspNetUser> userManager,
            IUserStore<AspNetUser> userStore,
            SignInManager<AspNetUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, 
            ICartService cartService,
            RoleManager<AspNetRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _cartService = cartService;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // Thêm các trường mới
            [Required]
            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public DateTime Dob { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public bool Gender { get; set; } // Giới tính: True cho Nam, False cho Nữ

            public string Role { get; set; } = "Customer";
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.LockoutEnabled = false;
                user.Gender = Input.Gender;
                user.Dob = Input.Dob;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var roleExists = await _roleManager.RoleExistsAsync(Input.Role);
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new AspNetRole { Name = Input.Role, NormalizedName = Input.Role.ToUpper() });
                    }

                    // Tạo giỏ hàng cho người dùng ngay sau khi tạo tài khoản
                    var cart = await _cartService.CreateCartAsync(await _userManager.GetUserIdAsync(user));
                    // 🛑 Gán user vào Role "customer"
                    await _userManager.AddToRoleAsync(user, Input.Role);

                    // Gửi token xác nhận email
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    // Nếu yêu cầu xác nhận email là bắt buộc, redirect đến trang xác nhận email.
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }



        private AspNetUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<AspNetUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AspNetUser)}'. Ensure that '{nameof(AspNetUser)}' is not an abstract class and has a parameterless constructor.");
            }
        }

        private IUserEmailStore<AspNetUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AspNetUser>)_userStore;
        }
    }

}
