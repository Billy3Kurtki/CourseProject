using Microservice.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen();*/

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.AddDbContext<StudentContext>(options =>
{
    var connetionString = "Server=localhost;port=3306;Database=coureproject;User Id=root;Password=root;";
    options.UseMySql(connetionString, ServerVersion.AutoDetect(connetionString));
});

/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

//app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
