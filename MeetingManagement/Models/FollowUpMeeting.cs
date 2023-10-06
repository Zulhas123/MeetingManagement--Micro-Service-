using Base.API.Models;

namespace MeetingManagement.Models
{
    public class FollowUpMeeting:FullAudit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MettingId { get; set; }
    }
}
