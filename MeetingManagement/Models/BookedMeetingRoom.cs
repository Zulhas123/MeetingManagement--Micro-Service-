using Base.API.Models;

namespace MeetingManagement.Models
{
    public class BookedMeetingRoom
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public string? TimeSlot { get; set; }
    }
}
