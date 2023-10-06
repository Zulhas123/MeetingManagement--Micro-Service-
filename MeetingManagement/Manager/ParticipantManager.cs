using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class ParticipantManager : BaseManager<Participant>, IParticipantManager
    {
        public ParticipantManager(ApplicationDbContext db):base(new BaseRepository<Participant>(db)) { }
        public Participant GetById(int id)
        {
           return GetFirstOrDefault(x => x.Id == id);

        }

        public Participant GetByUser(int userId)
        {
            return GetFirstOrDefault(x => x.ReferenceId == userId);

        }

        public ICollection<Participant> GetAll()
        {
            return Get(c => true);
        }
    }
}
