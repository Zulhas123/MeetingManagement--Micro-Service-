using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class MeetingRoomManager:BaseManager<MeetingRoom>, IMeetingRoomManager
    {
        public MeetingRoomManager(ApplicationDbContext db) : base(new BaseRepository<MeetingRoom>(db))
        {
        }

        public MeetingRoom GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }
    }
}
