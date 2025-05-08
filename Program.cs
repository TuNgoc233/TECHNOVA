using TECHNOVA.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using TECHNOVA.Data;
using Microsoft.AspNetCore.Authentication.Google;
using TECHNOVA.Models;
using TECHNOVA.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TechnovaContext>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("TECHNOVA"));
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}
);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Configure EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
	options.LoginPath = "/Customer/Login";
	options.AccessDeniedPath = "/AccessDenied";
})
.AddGoogle(options =>
{
    options.ClientId = "111734754077-lirrvkb5j79fpqs7vs543q1tt5m7lq7m.apps.googleusercontent.com";
    options.ClientSecret = "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";
})
.AddFacebook(options =>
{
    options.AppId = "662262743066911";
    options.AppSecret = "0a66a4511154930b7cd298658ce560dd";
});
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

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
