// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
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
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
       // private readonly IUserStore<AspNetUser> _userStore;
        //private readonly IUserEmailStore<AspNetUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;

        public ExternalLoginModel(
            SignInManager<AspNetUser> signInManager,
            UserManager<AspNetUser> userManager,
            //IUserStore<AspNetUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
             IUserService userService,
             ICartService cartService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
           // _userStore = userStore;
            //_emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
            _userService = userService;
            _cartService = cartService;
        }

 
        [BindProperty]
        public InputModel Input { get; set; }

      
        public string ProviderDisplayName { get; set; }

   
        public string ReturnUrl { get; set; }

     
        [TempData]
        public string ErrorMessage { get; set; }
 
        public class InputModel
        {
             
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            // Kiểm tra lỗi từ provider
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Lấy thông tin từ provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Lấy email từ Google (hoặc các provider khác)
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                TempData["ErrorMessage"] = "Your account has been locked. Please contact support.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Kiểm tra xem người dùng đã có tài khoản chưa
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                // Nếu người dùng không tồn tại, tạo một tài khoản mới
                user = new AspNetUser { UserName = email, Email = email };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    TempData["ErrorMessage"] = "Failed to create user.";
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                }

                // Cập nhật email đã được xác nhận ngay lập tức
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationResult = await _userManager.ConfirmEmailAsync(user, confirmationToken);
                if (!confirmationResult.Succeeded)
                {
                    TempData["ErrorMessage"] = "Failed to confirm email.";
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                }

                // Thêm thông tin đăng nhập của người dùng từ provider vào hệ thống
                var addLoginResult = await _userManager.AddLoginAsync(user, info);
                if (!addLoginResult.Succeeded)
                {
                    TempData["ErrorMessage"] = "Failed to add external login.";
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                }

                user.LockoutEnabled = false;
                // Tạo giỏ hàng cho người dùng ngay sau khi tạo tài khoản
                await _cartService.CreateCartAsync(await _userManager.GetUserIdAsync(user));
                // 🛑 Gán user vào Role "customer"
                await _userManager.AddToRoleAsync(user, "Customer");
                // Đăng nhập người dùng ngay sau khi đăng ký
                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);


            }
            else
            {
                // Nếu người dùng đã có tài khoản, đăng nhập ngay
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = "External login failed.";
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                }

                // Đăng nhập người dùng
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
            }

            return LocalRedirect(returnUrl); // Đưa người dùng quay lại trang mà họ yêu cầu
        }



        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new AspNetUser { UserName = Input.Email, Email = Input.Email };
                var password = await _userService.AutoCreatePasswords();

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        /*var roleExists = await _roleManager.RoleExistsAsync("customer");
                        if (!roleExists)
                        {
                            await _roleManager.CreateAsync(new AspNetRole { Name = "customer", NormalizedName = "customer".ToUpper() });
                        }*/

                        // 🛑 Gán user vào Role "customer"
                        await _userManager.AddToRoleAsync(user, "Customer");

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);


                        string subject = "Welcome to Our Platform!";
                        string message = $@"
                        <h3>Hello {Input.Email},</h3>
                        <p>Your account has been successfully created.</p>
                        <p><strong>Username:</strong> {Input.Email}</p>
                        <p><strong>Password:</strong> {password}</p>
                        <p>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.</p>
                        <p>Please change your password after logging in.</p>
                        <p>Best regards,</p>";

                        await _emailSender.SendEmailAsync(Input.Email, subject, message);

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
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
                throw new InvalidOperationException($"Can't create an instance of '{nameof(AspNetUser)}'. " +
                    $"Ensure that '{nameof(AspNetUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }

        /*private IUserEmailStore<AspNetUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<AspNetUser>)_userStore;
        }*/
    }
}
