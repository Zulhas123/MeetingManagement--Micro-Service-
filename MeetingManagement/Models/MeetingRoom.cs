using Base.API.Models;

namespace MeetingManagement.Models
{
    public class MeetingRoom:FullAudit
    {
        public int Id { get; set; }
        public int RoomNo { get; set; }
        public int FloorNo { get; set; }
        public int Capacity { get; set; }
        public int MeetingRoomTypeId { get; set; }
        public string Features { get; set; }
    }
}
