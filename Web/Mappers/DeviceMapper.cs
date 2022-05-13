using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;
using Web.Models.DB.Tables;

namespace Web.Mappers;

public class DeviceMapper
{
    public DeviceMapper(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }
    
    public async Task<IEnumerable<DeviceViewModel>> GetDevicesAsync()
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

        return model;
    }

    public async Task<bool> DeleteDeviceAsync(int id)
    {
        var device = await _dbApplicationContext.Devices
            .Where(elem => elem.ID == id).FirstOrDefaultAsync();

        if (device == null) return false;
        
        _dbApplicationContext.Devices.Remove(device);
        await _dbApplicationContext.SaveChangesAsync();

        return true;
    }

    public async Task<DeviceViewModel?> GetDeviceAsync(int id)
    {
        var device = await _dbApplicationContext.Devices
            .Where(elem => elem.ID == id).FirstOrDefaultAsync();

        return device != null ? new DeviceViewModel()
        {
            Address = device.Address,
            Description = device.Description,
            Enabled = device.Enabled,
            ID = device.ID,
            Title = device.Title
        } : null;
    }

    public async Task<bool> EditDeviceAsync(DeviceViewModel model)
    {
        var device = await _dbApplicationContext.Devices
            .Where(elem => elem.ID == model.ID).FirstOrDefaultAsync();
        if (device == null) return false;
        
        device.Address = model.Address;
        device.Description = model.Description;
        device.Enabled = model.Enabled;
        device.Title = model.Title;

        await _dbApplicationContext.SaveChangesAsync();
        return true;
    }

    public async Task AddDeviceAsync(DeviceViewModel model)
    {
        _dbApplicationContext.Devices.Add(new Device()
        {
            Title = model.Title,
            Enabled = model.Enabled,
            Address = model.Address
        });
        await _dbApplicationContext.SaveChangesAsync();
    }
    
    private DbApplicationContext _dbApplicationContext;
}