using MeetingManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagement.Data
{
    public class ApplicationDbContext:DbContext
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
        public DbSet<MeetingApprovalLayer> MeetingApprovalLayers { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<MeetingRoomType> MeetingRoomTypes { get; set; }
        public DbSet<MeetingTimeSlot> MeetingTimeSlots { get; set; }
        public DbSet<BookedMeetingRoom> BookedMeetingRooms { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Client> ExternalUsers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<MeetingSummery> MeetingSummery { get; set; }
        public DbSet<FollowUpMeeting> FollowUpMeeting { get; set; }
    }
}
