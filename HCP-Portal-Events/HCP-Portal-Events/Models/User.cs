using Microsoft.Extensions.Logging;

namespace HCP_Portal_Events.Models
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Email {  get; set; }
        public long PhoneNumber {  get; set; }

        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }
    }
}
