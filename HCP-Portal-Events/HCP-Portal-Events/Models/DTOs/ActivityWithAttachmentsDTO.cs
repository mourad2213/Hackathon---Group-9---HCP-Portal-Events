namespace HCP_Portal_Events.Models.DTOs
{
    public class ActivityWithAttachmentsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DayorModule_No { get; set; }
        public DateTime ActivityStartTime { get; set; }
        public string Description { get; set; }
        public List<AttachmentDTO> Attachments { get; set; }
        public List<SpeakerDTO> Speakers { get; set; } 
    }

}
