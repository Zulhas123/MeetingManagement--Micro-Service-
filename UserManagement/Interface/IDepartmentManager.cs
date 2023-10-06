using Base.API.Interface.Manager;
using Base.API.Manager;
using System.Data;
using UserManagement.Models;

namespace UserManagement.Interface
{
    interface IDepartmentManager:IBaseManager<Department>
    {
        Department GetById(int id);
       ICollection<Department> GetAll();
        
    }
}
