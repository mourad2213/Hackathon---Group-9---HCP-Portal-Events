namespace HCP_Portal_Events.Models.DTOs
{

    public class UserUpdateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }

        public IFormFile ProfilePicture { get; set; } 
        public string ProfilePicturePath { get; set; }
    }


}
