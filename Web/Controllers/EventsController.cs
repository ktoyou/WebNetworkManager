using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;

namespace Web.Controllers;

public class EventsController : Controller
{
    public EventsController(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }

    public async Task<IActionResult> Index()
    {
        var model = new List<EventsViewModel>();
        var events = await _dbApplicationContext.Events.ToListAsync();
        
        events.ForEach(elem =>
        {
            model.Add(new EventsViewModel()
            {
                ID = elem.ID,
                Begin = elem.Begin,
                End = elem.End,
                Level = elem.Level,
                Message = elem.Message
            });
        });
        ViewBag.Events = model;
        return View();
    }

    private readonly DbApplicationContext _dbApplicationContext;
}