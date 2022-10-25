using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using SMStore.Data;
using SMStore.Entities;
using SMStore.Service.Repositories;
using SMStore.Service.ValidationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();
builder.Services.AddHttpClient();  //Api mize istek gönderebilmek için gerekli servis!!

var app = builder.Build();

builder.Services.AddDbContext<DatabaseContext>(); 

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));  

builder.Services.AddTransient(typeof(ICategoryRepository), typeof(CategoryRepository));

builder.Services.AddScoped<IValidator<AppUser>, AppUserValidator>();
// builder.Services.AddScoped<IValidator<AdminLoginViewModel>, AdminLoginViewModelValidator>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(u =>
{
    u.LoginPath = "/Admin/Login"; // LoginPath in case user hasn't been logged in, yetkisiz kullanıcıları yönlendirir.
    u.AccessDeniedPath = "/AccessDenied"; // yetki kontrolü yaparsak yetkisi olmayanları bu sayfaya yönlendirir.
    u.LogoutPath = "/Admin/Logout";
    u.Cookie.Name = "Admin"; // Oluşacak cookie ye Admin ismini verdik.
    u.Cookie.MaxAge = TimeSpan.FromDays(3); // Oluşacak cookie nin yaşam süresi
    u.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // Admin yetkisi, bu yetkiye göre kontrol yapacaksak admin controller larda authorize attribute ün bu yetkiyi eklemeliyiz.
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User"));
}
);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Oturum açar login olan kullanıcı
app.UseAuthorization();

app.MapControllerRoute(
      name: "admin",
      pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
