using Base.API.Models;

namespace MeetingManagement.Models
{
    public class Participant
    {
       
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public int ReferenceId { get; set; }
        public bool IsUser { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsRejected { get; set; }
        public string? RejectNote { get; set; }

    }
}
