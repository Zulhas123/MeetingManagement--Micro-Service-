using Base.API.Manager;
using Base.API.Repository;
using System.Data;
using UserManagement.Data;
using UserManagement.Interface;
using UserManagement.Models;

namespace UserManagement.Manager
{
    public class DepartmentManager : BaseManager<Department>, IDepartmentManager
    {
        public DepartmentManager(AppDbContext db) : base(new BaseRepository<Department>(db))
        {
        }
        public Department GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }

        public ICollection<Department> GetAll()
        {
            return Get(c =>true);
        }

        
    }
}
