using AccessManagement.Models;
using Base.API.Interface.Manager;

namespace AccessManagement.Interface
{
    public interface IUserAccessManager:IBaseManager<UserAccess>
    {
        UserAccess GetById(int id);
        UserAccess CheckAccessUser(string cardNo);
        ICollection<UserAccess> GetAll();
    }
}
