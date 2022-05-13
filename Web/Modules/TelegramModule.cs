using Telegram.Bot;
using Telegram.Bot.Types;
using Web.Models.DB;

namespace Web.Modules;

public class TelegramModule : IModule
{
    public TelegramModule(DbConfiguration dbConfiguration)
    {
        _dbApplicationContext = new DbApplicationContext(dbConfiguration);
    }
    
    public async Task Run()
    {
        while (true)
        {
            var chats = _dbApplicationContext.TelegramChats.ToList();
            var events = _dbApplicationContext.Events.ToList();
            foreach (var chat in chats)
            {
                foreach (var e in events)
                {
                    _bot = new TelegramBotClient(chat.ApiKey);
                    await _bot.SendTextMessageAsync(new ChatId(chat.ChatID), e.Message);
                }
            }
            await Task.Delay(300000);
        }
    }

    private ITelegramBotClient _bot;
    
    private readonly DbApplicationContext _dbApplicationContext;
}