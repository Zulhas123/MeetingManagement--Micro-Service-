using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class BookedMeetingRoomManager:BaseManager<BookedMeetingRoom>,IBookedMeetingRoomManager
    {
        public BookedMeetingRoomManager(ApplicationDbContext db):base(new BaseRepository<BookedMeetingRoom>(db))
        {

        }

        public BookedMeetingRoom GetById(int id)
        {
            return GetFirstOrDefault(x=>x.Id == id);
        }

        public BookedMeetingRoom GetByMeetingId(int id)
        {
            return GetFirstOrDefault(c=>c.MeetingId== id);
        }

        public ICollection<BookedMeetingRoom> GetAll()
        {
            return Get(c => true);
        }
    }
}
