using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class MeetingRoomTypeManager:BaseManager<MeetingRoomType>, IMeetingRoomTypeManager
    {
        public MeetingRoomTypeManager(ApplicationDbContext db) : base(new BaseRepository<MeetingRoomType>(db))
        {
        }

        public MeetingRoomType GetById(int id)
        {
            return GetFirstOrDefault(c=>c.IsActive && c.Id == id);
        }
    }
}
