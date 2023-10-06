using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class MeetingTimeSlotManager : BaseManager<MeetingTimeSlot>, IMeetingTimeSlotManager
    {
        public MeetingTimeSlotManager(ApplicationDbContext db) : base(new BaseRepository<MeetingTimeSlot>(db))
        {
        }

        public MeetingTimeSlot GetById(int id)
        {
            return GetFirstOrDefault(x => x.IsActive && x.Id == id);
        }
        private MeetingTimeSlot GetBySlot(TimeSpan fromTime, TimeSpan toTime)
        {
            return GetFirstOrDefault(x => x.SlotStart==fromTime && x.SlotEnd == toTime);
        }
        public string GetTimeSlotIdsByTimeRange(TimeSpan fromTime, TimeSpan toTime)
        {
            string ids = "";
            var startSlot = fromTime;
            while (startSlot<toTime)
            {
              
                var thirtyMinutes = new TimeSpan(0, 30, 0);
                var endSlot = startSlot.Add(thirtyMinutes);
                var getSlot = GetBySlot(startSlot,endSlot);
                ids += ","+getSlot.Id;
                startSlot = endSlot;
            }
            return ids;
        }
    }
}
