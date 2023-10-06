using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    interface IMeetingTimeSlotManager:IBaseManager<MeetingTimeSlot>
    {
        MeetingTimeSlot GetById(int id);
        string GetTimeSlotIdsByTimeRange(TimeSpan fromTime, TimeSpan toTime);
    }
}
