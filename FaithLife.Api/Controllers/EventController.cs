using FaithLife.Api.Interfaces;
using FaithLife.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FaithLife.Api.Controllers
{
    [Authorize]
    [Route("api/event")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEvent _eventRepository;

        public EventController(IEvent eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEvents()
        {
            try
            {
                return Ok(await _eventRepository.GetAllEvents());

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
            try
            {
                var result = await _eventRepository.GetEventsId(id);

                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost("/CreateEvent")]
        public async Task<ActionResult<Event>> CreateEvent(Event Event)
        {
            await _eventRepository.AddEvent(Event);
            return Ok();
            try
            {
                if (Event == null)
                {
                    return BadRequest();
                }

                var createdEvent = await _eventRepository.AddEvent(Event);
                return Ok(createdEvent);
                return CreatedAtAction(nameof(Event), new { id = createdEvent.EventId }, createdEvent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Event>> UpdateEvent(int id, Event Event)
        {
            try
            {
                if(id != Event.EventId)
                {
                    return BadRequest("Event ID mismatch");
                }

                var updatedEvent = await _eventRepository.UpdateEvent(Event);

                if(updatedEvent == null)
                {
                    return NotFound($"Event with Id = {id} not found");
                }

                return await _eventRepository.UpdateEvent(Event);

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }

        }



        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Event>> DeleteEvent(int Id)
        {
            try
            {
                if (Id < 1 )
                {
                    return BadRequest("Event ID mismatch");
                }

                var DeleteEvent = await _eventRepository.DeleteEvent(Id);

                if (DeleteEvent == null)
                {
                    return NotFound($"Event with Id = {Id} not found");
                }

                var DeleteEventResult = await _eventRepository.DeleteEvent(Id);

                return Ok(DeleteEventResult);


            }

            catch (Exception)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");

                return Ok();

            }

        }
    }
}
