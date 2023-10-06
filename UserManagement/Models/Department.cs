using Base.API.Models;
namespace UserManagement.Models
{
    public class Department:FullAudit
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
