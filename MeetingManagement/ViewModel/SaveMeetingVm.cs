using MeetingManagement.Models;

namespace MeetingManagement.ViewModel
{
    public class SaveMeetingVm
    {


        public int MeetingId { get; set; }
        public List<Participant> Participants { get; set; }
        public List<Client> Clients { get; set; }


    }
   
}
