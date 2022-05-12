using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;
using Web.Tools;

namespace Web.Controllers;

public class AccountController : Controller
{
    public AccountController(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        if(User.Identity.IsAuthenticated) return Redirect($"~/");
        return View(new UserViewModel());
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        
        model.Password = UserTools.HashUserPassword(model.Password);
        var user = await _dbApplicationContext.Users
                .Where(user => user.Login == model.Login && user.Password == model.Password)
                .FirstOrDefaultAsync();

        if (user == null)
        {
            ModelState.AddModelError("Login", "Неверные логин и/или пароль");
            return View(model);
        }
        
        await Authenticate(model.Login);
        return Redirect($"~/");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View("Login");
    }

    private async Task Authenticate(string userName)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
        };
        var identity = new ClaimsIdentity(claims, "ApplicationCookie");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
    }

    private readonly DbApplicationContext _dbApplicationContext;
}