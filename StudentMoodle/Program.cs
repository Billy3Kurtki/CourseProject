using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentMoodle.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.AccessDeniedPath = "/Home/AccessDenied";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connetionString = "Server=localhost;port=3306;Database=coureproject;User Id=root;Password=root;";
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
});


builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("admin", build =>
    {
        build.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "admin"));
    });

    options.AddPolicy("lector", build =>
    {
        build.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "admin") 
                                    || x.User.HasClaim(ClaimTypes.Role, "lector"));
    });

    options.AddPolicy("student", build =>
    {
        build.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "admin")
                                    || x.User.HasClaim(ClaimTypes.Role, "lector")
                                    || x.User.HasClaim(ClaimTypes.Role, "student"));
    });
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
