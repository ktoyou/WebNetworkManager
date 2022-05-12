using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;
using Web.Models.DB.Tables;
using Web.Tools;

namespace Web.Controllers;

public class UsersController : Controller
{
    public UsersController(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _dbApplicationContext.Users.ToListAsync();
        var model = new List<UserViewModel>();
        
        users.ForEach(elem =>
        {
            model.Add(new UserViewModel()
            {
                Login = elem.Login,
                ID = elem.ID
            });
        });
        
        ViewBag.Users = model;
        return View();
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _dbApplicationContext.Users.Where(elem => elem.ID == id).FirstOrDefaultAsync();
        if (user == null) return RedirectPermanent("/users");

        _dbApplicationContext.Users.Remove(user);
        await _dbApplicationContext.SaveChangesAsync();
        
        return RedirectPermanent("/users");
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(UserViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);

        var user = new User();
        user.Login = model.Login;
        user.Password = UserTools.HashUserPassword(model.Password);

        _dbApplicationContext.Add(user);
        await _dbApplicationContext.SaveChangesAsync();

        return RedirectPermanent("/users");
    }

    private readonly DbApplicationContext _dbApplicationContext;
}