using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    public interface IBookedMeetingRoomManager:IBaseManager<BookedMeetingRoom>
    {
        BookedMeetingRoom GetById(int id);
        BookedMeetingRoom GetByMeetingId(int id);
        ICollection<BookedMeetingRoom> GetAll();
    }
}
