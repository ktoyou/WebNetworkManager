using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class TelegramChatViewModel
{
    public int ID { get; set; }
    public long ChatID { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Не указан ключ бота")]
    public string ApiKey { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Не указано название чата")]
    [MinLength(3, ErrorMessage = "Минимальное название чата - 3")]
    [MaxLength(255, ErrorMessage = "Максимальное название чата - 255")]
    public string Title { get; set; }
}