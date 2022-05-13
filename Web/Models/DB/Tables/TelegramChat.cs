using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models.DB.Tables;

[Table("telegram_chats")]
public class TelegramChat
{
    [Key] public int ID { get; set; }

    [Column("chat_id")] public long ChatID { get; set; }

    [Column("api_key")] public string ApiKey { get; set; }

    [Column("title")] public string Title { get; set; }
}