using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    public interface IParticipantManager:IBaseManager<Participant>
    {
        Participant GetById(int id);
        ICollection<Participant> GetAll();
        public Participant GetByUser(int userId);
    }
}
