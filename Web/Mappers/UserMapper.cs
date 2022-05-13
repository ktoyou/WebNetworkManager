using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;
using Web.Models.DB.Tables;

namespace Web.Mappers;

public class UserMapper
{
    public UserMapper(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }
    
    public string HashUserPassword(string password)
    {
        using (MD5 md5 = new MD5CryptoServiceProvider())
        {
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            return Convert.ToHexString(md5.ComputeHash(buffer));
        }
    }

    public async Task<bool> LoginAsync(UserViewModel model)
    {
        model.Password = HashUserPassword(model.Password);
        var user = await _dbApplicationContext.Users
            .Where(user => user.Login == model.Login && user.Password == model.Password)
            .FirstOrDefaultAsync();

        return user != null;
    }

    public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
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

        return model;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _dbApplicationContext.Users.Where(elem => elem.ID == id).FirstOrDefaultAsync();
        if (user == null) return false;

        _dbApplicationContext.Users.Remove(user);
        await _dbApplicationContext.SaveChangesAsync();

        return true;
    }

    public async Task AddUserAsync(UserViewModel model)
    {
        var user = new User();
        user.Login = model.Login;
        user.Password = HashUserPassword(model.Password);

        _dbApplicationContext.Add(user);
        await _dbApplicationContext.SaveChangesAsync();
    }

    private readonly DbApplicationContext _dbApplicationContext;
}