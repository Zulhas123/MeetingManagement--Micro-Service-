namespace MeetingManagement.ViewModel
{
    public class AvailableRoomsInfoVm
    {
        public int MeetingRoomId { get; set; }      
        public int RoomNo { get; set; }
        public int FloorNo { get; set; }
        public int Capacity { get; set; }
        //public int MeetingRoomTypeId { get; set; }
        public string Feature { get; set; }
        public TimeSpan TimeSlotStart { get; set; }
        public TimeSpan TimeSlotEnd { get; set; }
    }
}
