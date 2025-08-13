using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models;
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
        public async Task<IActionResult> GetPreviousEvents()
        {
            try
            {
                var events = await _unitOfWork.EventRepository.GetAllPreviousEventsAsync();

                if (events == null || !events.Any())
                    return NotFound(new { Message = "No previous events found." });

                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while fetching previous events.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/events/upcoming
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEvents()
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

        // GET: api/events/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid event ID." });

            try
            {
                var Event = await _unitOfWork.EventRepository.GetEventByIdAsync(id);

                if (Event == null)
                    return NotFound(new { Message = $"Event with ID {id} not found." });

                return Ok(Event);
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
        public async Task<IActionResult> GetEventWithActivities(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid event ID." });

            try
            {
                var Event = await _unitOfWork.EventRepository.GetEventWithActivitiesAndAttachments(id);

                if (Event == null)
                    return NotFound(new { Message = $"Event with ID {id} not found." });

                return Ok(Event);
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
    }
}
