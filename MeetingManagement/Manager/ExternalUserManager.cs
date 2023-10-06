using Base.API.Manager;
using Base.API.Repository;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Models;

namespace MeetingManagement.Manager
{
    public class ExternalUserManager:BaseManager<Client>,IExternalUserManager
    {
        public ExternalUserManager(ApplicationDbContext db):base(new BaseRepository<Client>(db))
        {

        }

        public Client GetById(int id)
        {
            return GetFirstOrDefault(x => x.Id == id);
        }

        public Client GetByPhoneNo(string phone)
        {
           return GetFirstOrDefault(x=>x.Phone == phone);
        }
    }
}
