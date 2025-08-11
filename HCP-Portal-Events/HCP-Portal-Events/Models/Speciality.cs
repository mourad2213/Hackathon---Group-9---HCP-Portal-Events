namespace HCP_Portal_Events.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Field { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}