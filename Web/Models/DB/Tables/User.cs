using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.DB.Tables;

[Table("users")]
public class User
{
    [Key] [Column("id")] public int ID { get; set; }
    
    [Column("password")] public string? Password { get; set; }
    
    [Column("login")] public string? Login { get; set; }
}