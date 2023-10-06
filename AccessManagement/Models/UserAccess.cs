using Base.API.Models;

namespace AccessManagement.Models
{
    public class UserAccess:FullAudit
    {
        public int Id { get; set; }
        public string CardNo { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
    }
}
