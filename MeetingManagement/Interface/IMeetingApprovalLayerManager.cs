using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    interface IMeetingApprovalLayerManager:IBaseManager<MeetingApprovalLayer>
    {
        MeetingApprovalLayer GetById(int Id);
        ICollection<MeetingApprovalLayer> GetByOrder(int order);
    }
}
