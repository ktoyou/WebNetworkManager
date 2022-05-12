using System.ComponentModel.DataAnnotations;

namespace Web.Models;

public class UserViewModel
{
    public int ID { get; set; }

    [Required(ErrorMessage = "Не указан логин", AllowEmptyStrings = false)]
    [MinLength(length: 4, ErrorMessage = "Минимальная длина логина 4")]
    [MaxLength(length: 255, ErrorMessage = "Максимальная длина логина 255")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Не указан пароль", AllowEmptyStrings = false)]
    [MinLength(length: 4, ErrorMessage = "Минимальная длина пароля 4")]
    [MaxLength(length: 255, ErrorMessage = "Максимальная длина пароля 255")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}