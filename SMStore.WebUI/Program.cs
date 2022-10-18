using SMStore.Data;
using SMStore.Service.Repositories;
using FluentValidation;
using SMStore.Entities;
using SMStore.Service.ValidationRules;
using SMStore.WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies; // Authentication kütüphanesini projeye ekliyoruz.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<DatabaseContext>(); // DbContext i ekliyoruz.

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Kendi yazdığımız repository servisini burada uygulamaya ekliyoruz. Burada eklemeden projede kullanmaya kalkarsak hata alırız!!.

// .Net Core ile birlikte 3 farklı Dependcy Injection yöntemi var sayılan olarak kullanmamıza sunulmuştur.
// Dependcy Injection Yöntemleri :
// 1 - AddSingleton : Bu yöntemi kullnırsak oluşturmak istediğimiz nesneden 1 tane oluşturulur ve her istediğimizde bu nesne bize gönderilir.
// 2 - AddTransient : Oluşturulması istenenden nesneden her istek için yeni 1 tane oluşturulur.
// 3 - AddScoped    : Oluşturulması istenenden nesne için gelen istediği bakılarak nesne daha önceden oluşturulmuşsa onu oluşturulmamışsa yeni bir tane oluşturup onu gönderir.

// FLuentValidation ile class i kontrol etmek için
builder.Services.AddScoped<IValidator<AppUser>, AppUserValidator>();
builder.Services.AddScoped<IValidator<AdminLoginViewModel>, AdminLoginViewModelValidator>();
// yukaridaki FluentValidation servislerini ekledikten sonra bu servisi kullanacağımız controller da servisi kullanarak validasyon yapabiliriz.

// Admin login oturum açma ayarları : 
 builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(u =>
 {
     u.LoginPath = "/Admin/Login"; // LoginPath in case user hasn't been logged in, yetkisiz kullanıcıları yönlendirir.
     u.AccessDeniedPath = "/AccessDenied"; // yetki kontrolü yaparsak yetkisi olmayanları bu sayfaya yönlendirir.
     u.LogoutPath = "/Admin/Logout";
     u.Cookie.Name = "Admin"; // Oluşacak cookie ye Admin ismini verdik.
     u.Cookie.MaxAge = TimeSpan.FromDays(3); // Oluşacak cookie nin yaşam süresi
     u.Cookie.IsEssential = true;
 });

// Admin login yetkilendirme ayarları :

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // Admin yetkisi, bu yetkiye göre kontrol yapacaksak admin controller larda authorize attribute ün bu yetkiyi eklemeliyiz.
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User"));
}
);

var app = builder.Build();

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
app.UseAuthorization(); // Oturum açan kullanıcıyı yetkilendirme -- o açılan yaratılan user a yetki verir

app.MapControllerRoute(
      name: "admin",
      pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
