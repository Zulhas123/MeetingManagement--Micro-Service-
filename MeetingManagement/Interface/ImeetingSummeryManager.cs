using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    public interface ImeetingSummeryManager: IBaseManager<MeetingSummery>
    {
        ICollection<MeetingSummery> GetAll();
        MeetingSummery GetById(int id);
    }
}
