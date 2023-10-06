using Base.API.Interface.Manager;
using Base.API.Manager;
using System.Data;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    interface IMeetingRoomManager:IBaseManager<MeetingRoom>
    {
        MeetingRoom GetById(int id);
    }
}
