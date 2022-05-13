using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Mappers;
using Web.Models;
using Web.Models.DB;

namespace Web.Controllers;

public class EventsController : Controller
{
    public EventsController(DbApplicationContext dbApplicationContext, EventMapper eventMapper)
    {
        _dbApplicationContext = dbApplicationContext;
        _eventMapper = eventMapper;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Events = await _eventMapper.GetEventsAsync();
        return View();
    }

    private readonly EventMapper _eventMapper;
    private readonly DbApplicationContext _dbApplicationContext;
}