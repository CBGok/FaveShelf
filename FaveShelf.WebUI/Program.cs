using FaveShelf.Business.Managers;
using FaveShelf.Business.Services;
using FaveShelf.Data.Context;
using FaveShelf.Data.Entities;
using FaveShelf.Data.Repositories;
using FaveShelf.WebUI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Konsola loglama ekleyin

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger'ý ekleyin
builder.Services.AddHttpClient();
builder.Services.AddScoped<SpotifyService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // React'in çalýþtýðý URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FaveShelfContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<ISongRepository, SongRepository>(); 
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ISongService, SongManager>();
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.Lax; // Cookie'nin nasýl taþýnacaðýný belirtir
        options.AccessDeniedPath = new PathString(""); // Yetkisiz eriþimde yönlendirme yapýlacak path
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Sadece izin verilen orijin
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // Kimlik bilgilerine izin ver
        });
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Swagger middleware'ini ekleyin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// CORS'u kullanýn
// app.UseCors();


app.UseCors("AllowSpecificOrigin"); // CORS politikasýný uygulayýn


app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();


app.Run();
