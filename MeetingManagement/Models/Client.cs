using Base.API.Models;

namespace MeetingManagement.Models
{
    public class Client: FullAudit
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
