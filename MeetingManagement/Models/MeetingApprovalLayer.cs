using Base.API.Models;

namespace MeetingManagement.Models
{
    public class MeetingApprovalLayer:FullAudit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Order { get; set; }
    }
}
