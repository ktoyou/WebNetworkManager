using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Mappers;
using Web.Models;
using Web.Models.DB;

namespace Web.Controllers;

public class AccountController : Controller
{
    public AccountController(UserMapper userMapper)
    {
        _userMapper = userMapper;
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
        bool status = await _userMapper.LoginAsync(model);
        if (!status)
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
    
    private readonly UserMapper _userMapper;
}