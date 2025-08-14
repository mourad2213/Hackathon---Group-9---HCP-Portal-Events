using HCP_Portal_Events.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HCP_Portal_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRegistrationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/userregistration/book
        [HttpPost("book")]
        public async Task<IActionResult> BookEvent(int userId, int eventId)
        {
            if (userId <= 0 || eventId <= 0)
                return BadRequest(new { Message = "Invalid user or event ID." });

            try
            {
                var user = await _unitOfWork.UserRepositiory.GetUserByIdAsync(userId);
                if (user == null)
                    return NotFound(new { Message = "User not found." });
                var eventDetails = await _unitOfWork.EventRepository.GetEventByIdAsync(eventId);
                if (eventDetails == null)

                    return NotFound(new { Message = "Event not found." });

                var result = await _unitOfWork.UserRegistrationRepository.BookEventAsync(userId, eventId);

                if (!result)
                    return Conflict(new { Message = "User is already registered for this event." });

                await _unitOfWork.CompleteAsync();
                return Ok(new { Message = "User successfully registered for the event." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while booking the event.",
                    Details = ex.Message
                });
            }
        }

        // POST: api/userregistration/unbook
        [HttpPost("unbook")]
        public async Task<IActionResult> UnbookEvent(int userId, int eventId)
        {
            if (userId <= 0 || eventId <= 0)
                return BadRequest(new { Message = "Invalid user or event ID." });

            try
            {
                var user = await _unitOfWork.UserRepositiory.GetUserByIdAsync(userId);
                if (user == null)
                    return NotFound(new { Message = "User not found." });

                var eventDetails = await _unitOfWork.EventRepository.GetEventByIdAsync(eventId);
                if (eventDetails == null)
                    return NotFound(new { Message = "Event not found." });

                var result = await _unitOfWork.UserRegistrationRepository.UnbookEventAsync(userId, eventId);

                if (!result)
                    return NotFound(new { Message = "User is not registered for this event or already unbooked." });

                await _unitOfWork.CompleteAsync();
                return Ok(new { Message = "User successfully unbooked from the event." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while unbooking the event.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/userregistration/isregistered
        [HttpGet("isregistered")]
        public async Task<IActionResult> IsUserRegistered(int userId, int eventId)
        {
            if (userId <= 0 || eventId <= 0)
                return BadRequest(new { Message = "Invalid user or event ID." });

            try
            {
                var user = await _unitOfWork.UserRepositiory.GetUserByIdAsync(userId);
                if (user == null)
                    return NotFound(new { Message = "User not found." });
                var eventDetails = await _unitOfWork.EventRepository.GetEventByIdAsync(eventId);
                if (eventDetails == null)
                    return NotFound(new { Message = "Event not found." });

                var isRegistered = await _unitOfWork.UserRegistrationRepository.IsUserRegisteredAsync(userId, eventId);
                return Ok(new { IsRegistered = isRegistered });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred while checking registration.",
                    Details = ex.Message
                });
            }
        }        
        
    }
}