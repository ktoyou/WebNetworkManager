using Telegram.Bot;
using Telegram.Bot.Types;
using Web.Models.DB;

namespace Web.Modules;

public class TelegramModule : IModule
{
    public TelegramModule(DbConfiguration dbConfiguration)
    {
        _dbConfiguration = dbConfiguration;
    }
    
    public async Task Run()
    {
        while (true)
        {
            DbApplicationContext dbApplicationContext = new DbApplicationContext(_dbConfiguration);
            var chats = dbApplicationContext.TelegramChats.ToList();
            var events = dbApplicationContext.Events.ToList();
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
    
    private readonly DbConfiguration _dbConfiguration;
}