using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Controllers & Views
builder.Services.AddControllersWithViews();

// Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Session
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Giriþ yapýlmamýþsa buraya yönlendirilir
        options.AccessDeniedPath = "/Login/AccessDenied"; // Rol yetmiyorsa buraya yönlendirilir
    });

var app = builder.Build();

// Hata sayfasý yönlendirmesi (Home yoksa Login/Error olarak deðiþtiriyoruz)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Error"); // Burayý deðiþtirdik!
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication ve Authorization sýrasýyla
app.UseAuthentication(); // önce Authentication
app.UseAuthorization();  // sonra Authorization

// Oturum iþlemleri
app.UseSession();

// Varsayýlan route: Login sayfasý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

// PLC için ek route (opsiyonel)
app.MapControllerRoute(
    name: "PLC",
    pattern: "PLC/{action=BaglantiAc}/{id?}",
    defaults: new { controller = "PLC" });

app.Run();
