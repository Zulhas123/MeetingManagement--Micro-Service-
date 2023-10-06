using Base.API.Models;

namespace UserManagement.Models
{
    public class Designation:FullAudit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DesignationOrder { get; set; }
    }
}
