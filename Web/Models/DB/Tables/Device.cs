using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Web.Models.DB.Tables;

[Table("ping_objects")]
public class Device
{
    [Key] public int ID { get; set; }

    [Column("address")] public string? Address { get; set; }

    [Column("title")] public string? Title { get; set; }

    [Column("description")] public string? Description { get; set; }

    [Column("online")] public bool Online { get; set; }
    
    [Column("enabled")] public bool Enabled { get; set; }
}