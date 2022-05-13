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
                    await TrySendMessageAsync(_bot, chat.ChatID, e.Message);
                }
            }
            await Task.Delay(5000);
        }
    }

    public async Task TrySendMessageAsync(ITelegramBotClient _bot, long chatID, string message)
    {
        try
        {
            await _bot.SendTextMessageAsync(new ChatId(chatID), message);
        }
        catch (Exception e)
        {
            //Todo: Exception должен будет попадать в лог, наверное новую вкладку нужно будет
        }
    }

    private ITelegramBotClient _bot;
    
    private readonly DbApplicationContext _dbApplicationContext;
}