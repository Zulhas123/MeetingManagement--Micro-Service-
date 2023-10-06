using Base.API.Models;

namespace MeetingManagement.Models
{
    public class MeetingSummery
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public string? File { get; set; }
        public string? SummeryDetails { get; set; }
    }
}
