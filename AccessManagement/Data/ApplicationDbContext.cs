using AccessManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var dbName = Configuration["UseDatabase"];
            if (dbName != null && dbName == "MySql")
            {
                var conMysql = Configuration.GetConnectionString("DefaultConnectionMySql");
                options.UseMySql(Configuration.GetConnectionString("DefaultConnectionMySql"), ServerVersion.AutoDetect(conMysql));
            }
            else
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            }

        }
        public DbSet<Gates> Gates { get; set; }
        public DbSet<UserAccess> UserAccess { get; set; }

    }
}
