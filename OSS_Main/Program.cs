using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OSS_Main.Data;
using OSS_Main.Hubs;
using OSS_Main.Models.Entity;
using OSS_Main.Proxy.Cloundinary;
using OSS_Main.Proxy.GGMail;
using OSS_Main.Proxy.GHN;
using OSS_Main.Proxy.Payos;
using OSS_Main.Proxy.vnPay;
using OSS_Main.Repository.Implementation;
using OSS_Main.Repository.Interface;
using OSS_Main.Service.Implementation;
using OSS_Main.Service.Interface;
using OSS_Main.Synchronize;


var builder = WebApplication.CreateBuilder(args);

//Feedback
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
//emailSender
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IEmailService, EmailService>();
//Product
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductService, ProductService>();

//Order
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
//Cart
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartService, CartService>();
//Category
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//User
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
//Receiver Information
builder.Services.AddScoped<IReceiveInforRepo, ReceiveInforRepo>();
builder.Services.AddScoped<IReceiveInforService, ReceiveInforService>();
//Cloundinary
builder.Services.AddScoped<ICloudinaryProxy, CloudinaryProxy>();
//Identity  
builder.Services.AddScoped<UserManager<AspNetUser>>();
builder.Services.AddScoped<SignInManager<AspNetUser>>();
//GHN
builder.Services.AddScoped<IGhnProxy, GhnApiProxy>();
//vnPay
builder.Services.AddScoped<IVnPayProxy, VnPayProxy>();
//PayOS
builder.Services.AddScoped<IPayosProxy, PayosProxy>();
//Gemini
builder.Services.AddHttpClient<GeminiService>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<Prn222ProjectContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của Session
    options.Cookie.HttpOnly = true; // Bảo mật cookie
    options.Cookie.IsEssential = true; // Đánh dấu cookie là cần thiết (GDPR compliance)
});


builder.Services.AddIdentity<AspNetUser, AspNetRole>(options =>
{
    //options.Password.RequiredLength = 6;
    //options.Password.RequireNonAlphanumeric = false;

    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;                // Không yêu cầu mật khẩu phải chứa số
    options.Password.RequireLowercase = false;            // Không yêu cầu chữ cái thường
    options.Password.RequireUppercase = false;            // Không yêu cầu chữ cái hoa
    options.Password.RequireNonAlphanumeric = false;      // Không yêu cầu ký tự đặc biệt (ví dụ: @, #, !, ...)
    options.Password.RequiredLength = 6;                  // Yêu cầu mật khẩu có độ dài tối thiểu là 6 ký tự

    options.Lockout.AllowedForNewUsers = false;  // Vô hiệu hóa Lockout cho người dùng mới
    options.Lockout.MaxFailedAccessAttempts = 5;  // Số lần thử đăng nhập sai tối đa
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
.AddEntityFrameworkStores<Prn222ProjectContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]
         ?? throw new Exception("Invalid google client Id");
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]
        ?? throw new Exception("Invalid google client secret");
    googleOptions.CallbackPath = "/signin-google";
    googleOptions.SaveTokens = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.Name = "UserAuthCookie";
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // Đường dẫn đến trang đăng nhập
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Trang bị từ chối truy cập
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddHostedService<ShippingSync>(); // woker service để chạy ngầm các task


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AspNetRole>>();

    await SeedData.SeedRolesAsync(roleManager);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapHub<CustomerHub>("/notificationHub");
app.MapHub<ShippingSyncHub>("/shippingHub");
app.MapHub<OrderHub>("/orderHub");

app.Run();
