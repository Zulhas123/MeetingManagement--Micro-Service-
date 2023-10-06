using Base.API.Models;

namespace AccessManagement.Models
{
    public class Gates:FullAudit
    {
       
        public int Id { get; set; }
        public  string ControllerSn { get; set; }
        public string Ip { get; set; }
        public string DoorNo { get; set; }
        public int MeetingRoomId { get; set; }
            
    }
}
