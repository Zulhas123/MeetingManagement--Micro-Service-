using Base.API.Interface.Manager;
using UserManagement.Models;

namespace UserManagement.Interface
{
    interface IDesignationManager:IBaseManager<Designation>
    {
        Designation GetById(int id);
        ICollection<Designation> GetAll();

    }
}
