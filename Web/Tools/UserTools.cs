using System.Security.Cryptography;
using System.Text;

namespace Web.Tools;

public class UserTools
{
    public static string HashUserPassword(string password)
    {
        using (MD5 md5 = new MD5CryptoServiceProvider())
        {
            byte[] buffer = Encoding.UTF8.GetBytes(password);
            return Convert.ToHexString(md5.ComputeHash(buffer));
        }
    }
}