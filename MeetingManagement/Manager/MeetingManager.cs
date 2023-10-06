using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class MeetingManager:BaseManager<Meeting>,IMeetingManager
    {
        public MeetingManager(ApplicationDbContext db):base(new BaseRepository<Meeting>(db))
        {

        }

        public ICollection<Meeting> GetAll()
        {
            return Get(c => true);
        }

        public Meeting GetById(int id)
        {
            return GetFirstOrDefault(x=>x.Id==id);
        }

        public ICollection<Meeting> GetListByDateRange(DateTime startdate, DateTime endDate)
        {
            return Get(c => c.IsActive && (c.Date >= startdate && c.Date <= endDate));
        }
        public ICollection<Meeting> GetListByTimeRange(TimeSpan starttime, TimeSpan endTime)
        {
            return Get(c => c.IsActive && (c.StartTime >= starttime && c.EndTime <= endTime));
        }
        public ICollection<Meeting> GetByOrder(int order)
        {


            return Get(c => c.IsActive && c.Order == order);
        }


        public Meeting GetMeetingById(int meetingId)
        {
            return GetFirstOrDefault(x => x.Id == meetingId);
        }
    }
}
