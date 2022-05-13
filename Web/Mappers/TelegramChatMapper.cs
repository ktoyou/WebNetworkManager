using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;
using Web.Models.DB.Tables;

namespace Web.Mappers;

public class TelegramChatMapper
{
    public TelegramChatMapper(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }

    public async Task<IEnumerable<TelegramChatViewModel>> GetTelegramChatsAsync()
    {
        var chats = await _dbApplicationContext.TelegramChats.ToListAsync();
        var model = new List<TelegramChatViewModel>();
        chats.ForEach(elem =>
        {
            model.Add(new TelegramChatViewModel()
            {
                ID = elem.ID,
                ChatID = elem.ChatID,
                ApiKey = elem.ApiKey,
                Title = elem.Title
            });
        });
        return model;
    }

    public async Task AddTelegramChatAsync(TelegramChatViewModel model)
    {
        _dbApplicationContext.TelegramChats.Add(new TelegramChat()
        {
            ChatID = model.ChatID,
            Title = model.Title,
            ApiKey = model.ApiKey
        });
        await _dbApplicationContext.SaveChangesAsync();
    }

    public async Task<bool> EditTelegramChatAsync(TelegramChatViewModel model)
    {
        var chat = await _dbApplicationContext.TelegramChats.Where(elem => elem.ID == model.ID).FirstOrDefaultAsync();
        if (chat == null) return false;

        chat.Title = model.Title;
        chat.ApiKey = model.ApiKey;
        chat.ChatID = model.ChatID;

        await _dbApplicationContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTelegramChatAsync(int id)
    {
        var chat = await _dbApplicationContext.TelegramChats.Where(elem => elem.ID == id).FirstOrDefaultAsync();
        if (chat == null) return false;

        _dbApplicationContext.TelegramChats.Remove(chat);
        await _dbApplicationContext.SaveChangesAsync();

        return true;
    }

    public async Task<TelegramChatViewModel> GetTelegramChatAsync(int id)
    {
        var chat = await _dbApplicationContext.TelegramChats.Where(elem => elem.ID == id).FirstOrDefaultAsync();
        return new TelegramChatViewModel()
        {
            ID = chat.ID,
            ApiKey = chat.ApiKey,
            ChatID = chat.ChatID,
            Title = chat.Title
        };
    }

    private readonly DbApplicationContext _dbApplicationContext;
}