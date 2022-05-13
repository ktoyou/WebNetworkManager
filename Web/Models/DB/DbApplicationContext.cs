using Microsoft.EntityFrameworkCore;
using Web.Models.DB.Tables;

namespace Web.Models.DB;

public class DbApplicationContext : DbContext
{
    public DbSet<Device> Devices { get; set; }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Event> Events { get; set; }

    public DbApplicationContext(DbConfiguration dbConfiguration)
    {
        _dbConfiguration = dbConfiguration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql($"Server={_dbConfiguration.Server};Database={_dbConfiguration.DataBase};Uid={_dbConfiguration.UID};Pwd={_dbConfiguration.Password};", ServerVersion.Parse("0.0.0"));
    }

    private readonly DbConfiguration _dbConfiguration;
}