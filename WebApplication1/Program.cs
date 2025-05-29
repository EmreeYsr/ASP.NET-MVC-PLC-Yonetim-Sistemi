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
        options.LoginPath = "/Login/Index"; // Giri� yap�lmam��sa buraya y�nlendirilir
        options.AccessDeniedPath = "/Login/AccessDenied"; // Rol yetmiyorsa buraya y�nlendirilir
    });

var app = builder.Build();

// Hata sayfas� y�nlendirmesi (Home yoksa Login/Error olarak de�i�tiriyoruz)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Login/Error"); // Buray� de�i�tirdik!
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication ve Authorization s�ras�yla
app.UseAuthentication(); // �nce Authentication
app.UseAuthorization();  // sonra Authorization

// Oturum i�lemleri
app.UseSession();

// Varsay�lan route: Login sayfas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

// PLC i�in ek route (opsiyonel)
app.MapControllerRoute(
    name: "PLC",
    pattern: "PLC/{action=BaglantiAc}/{id?}",
    defaults: new { controller = "PLC" });

app.Run();
