using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;
using Web.Models.DB.Tables;

namespace Web.Controllers;

[Authorize]
public class DevicesController : Controller
{
    public DevicesController(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var devices = await _dbApplicationContext.Devices.ToListAsync();
        var model = new List<DeviceViewModel>();
        devices.ForEach(elem =>
        {
            model.Add(new DeviceViewModel()
            {
                ID = elem.ID,
                Enabled = elem.Enabled,
                Address = elem.Address,
                Description = elem.Description,
                Online = elem.Online,
                Title = elem.Title
            });
        });
        ViewBag.Devices = model;
        return View();
    }

    public async Task<IActionResult> Delete(int id)
    {
        var device = await _dbApplicationContext.Devices
            .Where(elem => elem.ID == id).FirstOrDefaultAsync();

        _dbApplicationContext.Devices.Remove(device);
        
        if (device == null) Response.StatusCode = 404;
        await _dbApplicationContext.SaveChangesAsync();
        return RedirectPermanent("/devices");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var device = await _dbApplicationContext.Devices
            .Where(elem => elem.ID == id).FirstOrDefaultAsync();
        if (device == null)
        {
            return RedirectPermanent("/devices");
        }
        return View(new DeviceViewModel()
        {
            Description = device.Description,
            Enabled = device.Enabled,
            ID = device.ID,
            Address = device.Address,
            Title = device.Title
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DeviceViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        
        var device = await _dbApplicationContext.Devices
            .Where(elem => elem.ID == model.ID).FirstOrDefaultAsync();

        if (device == null)
        {
            return View(model);
        }

        device.Address = model.Address;
        device.Description = model.Description;
        device.Enabled = model.Enabled;
        device.Title = model.Title;

        await _dbApplicationContext.SaveChangesAsync();
        return RedirectPermanent("/devices");
    }

    
    [HttpGet]
    public IActionResult Add() => View(new DeviceViewModel());
    
    [HttpPost]
    public async Task<IActionResult> Add(DeviceViewModel model)
    {
        if (!TryValidateModel(model))
        {
            return View(model);
        }
        _dbApplicationContext.Devices.Add(new Device()
        {
            Title = model.Title,
            Enabled = model.Enabled,
            Address = model.Address
        });
        await _dbApplicationContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    private readonly DbApplicationContext _dbApplicationContext;
}