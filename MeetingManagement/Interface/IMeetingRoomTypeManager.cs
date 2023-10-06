using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    interface IMeetingRoomTypeManager : IBaseManager<MeetingRoomType>
    {
        MeetingRoomType GetById(int id);
    }
}
