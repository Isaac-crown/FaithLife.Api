using FaithLife.Api.Data;
using FaithLife.Api.Interfaces;
using FaithLife.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaithLife.Api.Repository
{
    public class EventRepository : IEvent
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Event> AddEvent(Event Event)
        {
          var result =  await _context.Events.AddAsync(Event);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public  async Task<Event> DeleteEvent(int Id)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x => x.EventId == Id);
            if(result != null)
            {
                _context.Events.Remove(result);
                 _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public  async Task<Event> GetEventsId(int Id)
        {
            return await _context.Events.FirstOrDefaultAsync(x => x.EventId == Id);
        }

     

        public async Task<Event> UpdateEvent(Event Event)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x=> x.EventId == Event.EventId);

            if(result != null)
            {
                result.EventName = Event.EventName;
                result.EventId = Event.EventId;
               // result.Services = Event.Services;
                result.EventDescription = Event.EventDescription;
                result.Created = Event.Created;
                result.Minister = Event.Minister;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
