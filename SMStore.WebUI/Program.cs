using Microsoft.AspNetCore.Authentication.Cookies;
using SMStore.Data;
using SMStore.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/// builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(u =>
/// {
///     u.LoginPath = "/Admin/Login"; // LoginPath in case user hasn't been logged in 
/// });

builder.Services.AddDbContext<DatabaseContext>(); // DbContext i ekliyoruz.

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Kendi yazdığımız repository servisini burada uygulamaya ekliyoruz. Burada eklemeden projede kullanmaya kalkarsak hata alırız!!.

// .Net Core ile birlikte 3 farklı Dependcy Injection yöntemi var sayılan olarak kullanmamıza sunulmuştur.
// Dependcy Injection Yöntemleri :
// 1 - AddSingleton : Bu yöntemi kullnırsak oluşturmak istediğimiz nesneden 1 tane oluşturulur ve her istediğimizde bu nesne bize gönderilir.
// 2 - AddTransient : Oluşturulması istenenden nesneden her istek için yeni 1 tane oluşturulur.
// 3 - AddScoped    : Oluşturulması istenenden nesne için gelen istediği bakılarak nesne daha önceden oluşturulmuşsa onu oluşturulmamışsa yeni bir tane oluşturup onu gönderir.

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

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
      name: "admin",
      pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
