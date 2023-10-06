using Base.API.Models;

namespace MeetingManagement.Models
{
    public class MeetingRoomType:FullAudit
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
