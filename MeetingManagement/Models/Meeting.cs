using Base.API.Models;
using Microsoft.Identity.Client;

namespace MeetingManagement.Models
{
    public class Meeting: FullAudit
    {
       
        public int Id { get; set; }
        public int MeetingRoomId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsApproved { get; set; }
        public string? CurrentlyApproval { get; set; }
        public string? AlreadyApproved { get; set; }
        public int Order { get; set; }
        public bool IsReject { get; set; }
        public string? RejectNote { get; set; }
        public int RejectBy { get;set; }
        public DateTime RejectDate { get; set; }    
        public bool IsSave { get; set; }
    }
}
