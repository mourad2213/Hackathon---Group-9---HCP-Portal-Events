namespace HCP_Portal_Events.Models.DTOs
{
        public class ActivityWithAttachmentsDTO
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public ICollection<string> Speakers { get; set; }
            public DateTime Date { get; set; }
            public List<AttachmentDTO> Attachments { get; set; }
        }
    
}
