using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.DB;

namespace Web.Mappers;

public class EventMapper
{
    public EventMapper(DbApplicationContext dbApplicationContext)
    {
        _dbApplicationContext = dbApplicationContext;
    }

    public async Task<IEnumerable<EventsViewModel>> GetEventsAsync()
    {
        var model = new List<EventsViewModel>();
        var events = await _dbApplicationContext.Events.ToListAsync();
        
        events.ForEach(elem =>
        {
            model.Add(new EventsViewModel()
            {
                ID = elem.ID,
                Begin = elem.Begin,
                End = elem.End,
                Level = elem.Level,
                Message = elem.Message
            });
        });

        return model;
    }

    private readonly DbApplicationContext _dbApplicationContext;
}