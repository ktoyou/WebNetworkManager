using Microsoft.AspNetCore.Mvc;
using Web.Mappers;
using Web.Models;

namespace Web.Controllers;

public class TelegramController : Controller
{
    public TelegramController(TelegramChatMapper telegramChatMapper)
    {
        _telegramChatMapper = telegramChatMapper;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Chats = await _telegramChatMapper.GetTelegramChatsAsync();
        return View();
    }

    [HttpGet] 
    public IActionResult Add() => View(new TelegramChatViewModel());

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var chat = await _telegramChatMapper.GetTelegramChatAsync(id);
        return chat == null ? RedirectToRoute("default", new {@controller = "Telegram", @action = "Index"}) : View(chat);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(TelegramChatViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        await _telegramChatMapper.AddTelegramChatAsync(model);
        return RedirectToRoute("default", new {@controller = "Telegram", @action = "Index"});
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(TelegramChatViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        await _telegramChatMapper.EditTelegramChatAsync(model);
        return RedirectToRoute("default", new {@controller = "Telegram", @action = "Index"});
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _telegramChatMapper.DeleteTelegramChatAsync(id);
        return RedirectToRoute("default", new {@controller = "Telegram", @action = "Index"});
    }
    
    private readonly TelegramChatMapper _telegramChatMapper;
}