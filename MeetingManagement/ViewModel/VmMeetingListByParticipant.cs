namespace MeetingManagement.ViewModel
{
    public class VmMeetingListByParticipant
    {
        public int Id { get; set; }
        public int MeetingRoomId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set;}
       public TimeSpan EndTime { get; set;}
        public string RoomNo { get; set;}
        public int MeetingRoomTypeId { get;set;}
        public string FloorNo { get; set;}
    }
    
    
}
