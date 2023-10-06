using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    public interface IMeetingManager:IBaseManager<Meeting>
    {
        Meeting GetById(int id);
        ICollection<Meeting> GetAll();
        ICollection<Meeting> GetListByDateRange(DateTime startdate, DateTime endDate);
        ICollection<Meeting> GetListByTimeRange(TimeSpan starttime, TimeSpan endTime);
        Meeting GetMeetingById(int meetingId);


    }
}
