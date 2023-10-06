using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class MeetingApprovalLayerManager:BaseManager<MeetingApprovalLayer>, IMeetingApprovalLayerManager
    {
        public MeetingApprovalLayerManager(ApplicationDbContext db) : base(new BaseRepository<MeetingApprovalLayer>(db))
        {
        }

        public MeetingApprovalLayer GetById(int Id)
        {
            return GetFirstOrDefault(x => x.IsActive && x.Id == Id);
        }
        public ICollection<MeetingApprovalLayer> GetByOrder(int order)
        {


            return Get(c => c.IsActive && c.Order == order);
        }
    }
}
