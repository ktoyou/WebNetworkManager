using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Mappers;
using Web.Models;
using Web.Models.DB;
using Web.Models.DB.Tables;

namespace Web.Controllers;

[Authorize]
public class DevicesController : Controller
{
    public DevicesController(DbApplicationContext dbApplicationContext, DeviceMapper deviceMapper)
    {
        _dbApplicationContext = dbApplicationContext;
        _deviceMapper = deviceMapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.Devices = await _deviceMapper.GetDevicesAsync();
        return View();
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _deviceMapper.DeleteDeviceAsync(id);
        return RedirectPermanent("/devices");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var device = await _deviceMapper.GetDeviceAsync(id);
        return device == null ? RedirectPermanent("/devices") : View(device);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DeviceViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        await _deviceMapper.EditDeviceAsync(model);
        return RedirectPermanent("/devices");
    }

    
    [HttpGet]
    public IActionResult Add() => View(new DeviceViewModel());
    
    [HttpPost]
    public async Task<IActionResult> Add(DeviceViewModel model)
    {
        if (!TryValidateModel(model)) return View(model);
        await _deviceMapper.AddDeviceAsync(model);
        return RedirectToAction("Index");
    }

    private readonly DbApplicationContext _dbApplicationContext;

    private readonly DeviceMapper _deviceMapper;
}