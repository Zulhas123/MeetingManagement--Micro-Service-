using AccessManagement.Data;
using AccessManagement.Interface;
using AccessManagement.Models;
using Base.API.Manager;
using Base.API.Repository;

namespace AccessManagement.Manager
{
    public class GateManager : BaseManager<Gates>, IGateManager
    {
        public GateManager(ApplicationDbContext db):base(new BaseRepository<Gates>(db)) { }
        public ICollection<Gates> GetAll()
        {
            return Get(c => true);
        }
        public Gates GetByControllerAndDoorNo(string controllerSn, string doorNo)
        {
            return GetFirstOrDefault(c => c.IsActive && c.ControllerSn == controllerSn && c.DoorNo == doorNo);
        }
        public Gates GetById(int id)
        {
            return GetFirstOrDefault(c=>c.Id==id);
        }
    }
}
