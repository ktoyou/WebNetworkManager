using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Mappers;
using Web.Models;
using Web.Models.DB;
using Web.Models.DB.Tables;

namespace Web.Controllers;

public class UsersController : Controller
{
    public UsersController(UserMapper userMapper)
    {
        _userMapper = userMapper;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Users = await _userMapper.GetUsersAsync();
        return View();
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        await _userMapper.DeleteUserAsync(id);
        return RedirectToRoute("default", new { @controller = "Users", @action = "Index" });
    }

    [HttpGet]
    public IActionResult Add() => View(new UserViewModel());

    [HttpPost]
    public async Task<IActionResult> Add(UserViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        await _userMapper.AddUserAsync(model);
        return RedirectToRoute("default", new { @controller = "Users", @action = "Index" });
    }
    
    private readonly UserMapper _userMapper;
}