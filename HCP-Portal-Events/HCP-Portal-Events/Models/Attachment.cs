namespace HCP_Portal_Events.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

    }
}
