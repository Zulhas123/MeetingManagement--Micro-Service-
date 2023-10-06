using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    public interface IFollowUpMeetingManager: IBaseManager<FollowUpMeeting>
    {
        FollowUpMeeting GetById(int id);
        ICollection<FollowUpMeeting> GetByMeetingId(int meetingId);
    }
}
