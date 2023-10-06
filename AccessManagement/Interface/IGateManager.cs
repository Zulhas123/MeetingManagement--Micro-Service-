using AccessManagement.Models;
using Base.API.Interface.Manager;

namespace AccessManagement.Interface
{
    public interface IGateManager:IBaseManager<Gates>
    {
        Gates GetById(int id);
       Gates GetByControllerAndDoorNo(string controllerSn, string doorNo);
        ICollection<Gates> GetAll();
    }
}
