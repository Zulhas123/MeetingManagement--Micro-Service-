using Base.API.Manager;
using Base.API.Repository;
using UserManagement.Data;
using UserManagement.Interface;
using UserManagement.Models;

namespace UserManagement.Manager
{
    public class DesignationManager : BaseManager<Designation>,IDesignationManager
    {
        public DesignationManager(AppDbContext db) : base(new BaseRepository<Designation>(db)) 
        { 

        }

        public ICollection<Designation> GetAll()
        {
            return Get(c => true);
        }

        public Designation GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }
    }
}
