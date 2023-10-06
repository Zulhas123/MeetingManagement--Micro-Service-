using Base.API.Models;

namespace MeetingManagement.Models
{
    public class MeetingTimeSlot:FullAudit
    {
        public int Id { get; set; }
        public TimeSpan SlotStart { get; set; }
        public TimeSpan SlotEnd { get; set; }
       
    }
}
