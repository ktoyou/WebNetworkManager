using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class DeviceViewModel
{
    public int ID { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Не указан IP-адрес")] 
    [RegularExpression("((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\\.|$)){4}", ErrorMessage = "Неверный формат IP")]
    public string? Address { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Не указано название девайса")] public string? Title { get; set; }

    public string? Description { get; set; }
    
    public bool Online { get; set; }
    
    public bool Enabled { get; set; }
}