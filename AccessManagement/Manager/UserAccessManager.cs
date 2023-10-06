using AccessManagement.Data;
using AccessManagement.Interface;
using AccessManagement.Models;
using Base.API.Manager;
using Base.API.Repository;

namespace AccessManagement.Manager
{
    public class UserAccessManager : BaseManager<UserAccess>, IUserAccessManager
    {
        public UserAccessManager(ApplicationDbContext db):base(new BaseRepository<UserAccess>(db)) { }

        public UserAccess CheckAccessUser(string cardNo)
        {
           return GetFirstOrDefault(c=>c.CardNo == cardNo && c.IsActive);
        }

        public ICollection<UserAccess> GetAll()
        {
            return Get(c => true);
        }

        public UserAccess GetById(int id)
        {
            return GetFirstOrDefault(c => c.Id == id);
        }
    }
}
