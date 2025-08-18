namespace HCP_Portal_Events.Models.DTOs
{
    public class UserReadDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }

        public string Speciality { get; set; }
    }
}
