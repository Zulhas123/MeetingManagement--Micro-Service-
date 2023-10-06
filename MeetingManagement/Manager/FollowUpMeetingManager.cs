using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class FollowUpMeetingManager: BaseManager<FollowUpMeeting>, IFollowUpMeetingManager
    {
        public FollowUpMeetingManager(ApplicationDbContext db) : base(new BaseRepository<FollowUpMeeting>(db))
        {

        }

        public FollowUpMeeting GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }

        public ICollection <FollowUpMeeting> GetByMeetingId(int meetingId)
        {
            return Get(x => x.Id == meetingId && x.IsActive==true);
        }

    }
}
