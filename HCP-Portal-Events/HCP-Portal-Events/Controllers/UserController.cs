using HCP_Portal_Events.DataAccess.Interfaces;
using HCP_Portal_Events.Models;
using HCP_Portal_Events.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HCP_Portal_Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try { 
            var user = await _unitOfWork.UserRepositiory.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new {message = "User Not Found" });
            }

            return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred While Getting User",
                    Details = ex.Message
                });
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _unitOfWork.UserRepositiory.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User Not Found" });
                }

                if (userUpdateDto.ProfilePicture != null)
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(userUpdateDto.ProfilePicture.FileName)}";
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "users");

                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await userUpdateDto.ProfilePicture.CopyToAsync(stream);
                    }

                    userUpdateDto.ProfilePicturePath = $"/images/users/{fileName}";
                }
                else
                {
                    userUpdateDto.ProfilePicturePath = user.ProfilePicture;
                }

                await _unitOfWork.UserRepositiory.UpdateUserAsync(id, userUpdateDto);
                await _unitOfWork.CompleteAsync();

                return Ok(new { message = "User updated successfully", profilePicture = userUpdateDto.ProfilePicturePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while updating the user",
                    Details = ex.Message
                });
            }
        }



        // GET: api/Users/5/previous-events
        [HttpGet("{userId}/previous-events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetUserPreviousEvents(int userId)
        {
            try { 
            var events = await _unitOfWork.UserRepositiory.GetUserPerviousEvents(userId);
            return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while getting the events.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/Users/5/upcoming-events
        [HttpGet("{userId}/upcoming-events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetUserUpcomingEvents(int userId)
        {
            try { 
            var events = await _unitOfWork.UserRepositiory.GetUserUpcomingEvents(userId);
            return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while getting the events.",
                    Details = ex.Message
                });
            }
        }

        // GET: api/Users/5/speciality-events
        [HttpGet("{userId}/speciality-events")]
        public async Task<ActionResult<IEnumerable<Event>>> GetUserSpecialityEvents(int userId)
        {
            try { 
            var user = await _unitOfWork.UserRepositiory.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var events = await _unitOfWork.UserRepositiory.GetUserSpecialityEvents(user);
            return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An unexpected error occurred while getting the events.",
                    Details = ex.Message
                });
            }
        }
    }
}