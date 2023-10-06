using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class MeetingSummeryManager: BaseManager<MeetingSummery>, ImeetingSummeryManager
    {
        public MeetingSummeryManager(ApplicationDbContext db) : base(new BaseRepository<MeetingSummery>(db))
        {

        }

        public ICollection<MeetingSummery> GetAll()
        {
            return Get(c => true);
        }

        public MeetingSummery GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }
    }
}
