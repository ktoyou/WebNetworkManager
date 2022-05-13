using Microsoft.AspNetCore.Authorization;
using Web.Mappers;
using Web.Models.DB;

var builder = WebApplication.CreateBuilder(args);
var config = DbConfiguration.LoadConfiguration("config.json");

// Add services to the container.
builder.Services.AddSingleton(config);
builder.Services.AddDbContext<DbApplicationContext>();
builder.Services.AddTransient<DeviceMapper>();
builder.Services.AddTransient<UserMapper>();
builder.Services.AddTransient<EventMapper>();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Cookies").AddCookie();
builder.Services.AddAuthorization();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
