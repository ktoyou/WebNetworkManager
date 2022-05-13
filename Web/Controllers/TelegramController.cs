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
        return chat == null ? RedirectPermanent("/telegram") : View(chat);
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(TelegramChatViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        await _telegramChatMapper.AddTelegramChatAsync(model);
        return RedirectPermanent("/telegram");
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(TelegramChatViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        await _telegramChatMapper.EditTelegramChatAsync(model);
        return RedirectPermanent("/telegram");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _telegramChatMapper.DeleteTelegramChatAsync(id);
        return RedirectPermanent("/telegram");
    }
    
    private readonly TelegramChatMapper _telegramChatMapper;
}