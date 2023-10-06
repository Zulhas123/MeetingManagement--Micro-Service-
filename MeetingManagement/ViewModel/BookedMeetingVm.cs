namespace MeetingManagement.ViewModel
{
    public class BookedMeetingVm
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }
        public int MeetingRoomId { get; set; }
        
    }
}
