using Base.API.Interface.Manager;
using MeetingManagement.Models;

namespace MeetingManagement.Interface
{
    public interface IExternalUserManager:IBaseManager<Client>
    {
        Client GetById(int id);   
        Client GetByPhoneNo(string phone);
    }
}
