using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HCP_Portal_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/events/previous
        [HttpGet("previous")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetPreviousEvents()
        {
            /*try
            {*/
                var events = await _unitOfWork.EventRepository.GetAllPreviousEventsAsync();

                if (events == null || !events.Any())
                    return NotFound(new { Message = "No previous events found." });

                return Ok(events);
            /*}
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while fetching previous events.",
                    Details = ex.Message
                });
            }*/
        }

        // GET: api/events/upcoming
        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetUpcomingEvents()
        {
            try
            {
                var events = await _unitOfWork.EventRepository.GetAllUpcomingEventsAsync();

                if (events == null || !events.Any())
                    return NotFound(new { Message = "No upcoming events found." });

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while fetching upcoming events.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/events/type/{type}
        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetEventsByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return BadRequest(new { Message = "Event type cannot be empty." });

            try
            {
                var events = await _unitOfWork.EventRepository.GetAllEventsByTypeAsync(type);

                if (events == null || !events.Any())
                    return NotFound(new { Message = $"No events found with type '{type}'." });

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while fetching events by type.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/events/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> GetEventsByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(new { Message = "Status cannot be empty." });

            try
            {
                var events = await _unitOfWork.EventRepository.GetAllEventsByStatusAsync(status);

                if (events == null || !events.Any())
                    return NotFound(new { Message = $"No events found with status '{status}'." });

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while fetching events by status.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/events/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EventReadDTO>> GetEventById(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid event ID." });

            try
            {
                var eventEntity = await _unitOfWork.EventRepository.GetEventByIdAsync(id);

                if (eventEntity == null)
                    return NotFound(new { Message = $"Event with ID {id} not found." });

                return Ok(eventEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while fetching the event.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/events/{id}/activities
        [HttpGet("{id:int}/activities")]
        public async Task<ActionResult<EventReadActivitesandAttachmentsDTO>> GetEventWithActivities(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid event ID." });

            try
            {
                var eventEntity = await _unitOfWork.EventRepository.GetEventWithActivitiesAndAttachments(id);

                if (eventEntity == null)
                    return NotFound(new { Message = $"Event with ID {id} not found." });

                return Ok(eventEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while fetching event activities.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/events/search?name=keyword
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EventReadDTO>>> SearchEvents(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new { Message = "Search term cannot be empty." });

            try
            {
                var events = await _unitOfWork.EventRepository.SearchEventsByNameAsync(name);

                if (events == null || !events.Any())
                    return NotFound(new { Message = $"No events found matching '{name}'." });

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while searching for events.",
                    Details = ex.Message
                });
            }
        }
    }
}
