using FaithLife.Api.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaithLife.Api.Interfaces
{
    public interface IEvent
    {
       Task<IEnumerable<Event>> GetAllEvents();

        Task<Event> GetEventsId(int Id);

        Task<Event> AddEvent(Event Event);

        Task<Event>  UpdateEvent(Event Event);

        Task<Event> DeleteEvent(int Id);

    }
}
