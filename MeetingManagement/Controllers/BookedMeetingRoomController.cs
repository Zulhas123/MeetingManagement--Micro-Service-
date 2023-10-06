using Base.API.Controllers;
using Base.API.Models;
using Base.API.SecurityExtension;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using MeetingManagement.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using System.Net.Http.Headers;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class BookedMeetingRoomController : BaseController
    {
        private readonly IBookedMeetingRoomManager _bookedMeetingRoomManager;
        private readonly IMeetingManager _meetingManager;
        private readonly IExternalUserManager _externalUserManager;
        private readonly IParticipantManager _participantManager;
        private readonly IMeetingTimeSlotManager _meetingTimeSlotManager;

        private readonly IMeetingApprovalLayerManager meetingApprovalLayerManager;


        public BookedMeetingRoomController(ApplicationDbContext db)
        {
            _bookedMeetingRoomManager = new BookedMeetingRoomManager(db);
            _meetingManager = new MeetingManager(db);
            _externalUserManager = new ExternalUserManager(db);
            _participantManager = new ParticipantManager(db);
            _meetingTimeSlotManager = new MeetingTimeSlotManager(db);
            meetingApprovalLayerManager = new MeetingApprovalLayerManager(db);

        }

        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult BookedMeeting(BookedMeetingVm bookedMeetingVm)
        {
            try
            {
                if (bookedMeetingVm == null)
                {
                    return BadRequest("Nothing to save");
                }
                var list = meetingApprovalLayerManager.GetByOrder(1);

                var userId = string.Join(",", list.Select(c => c.UserId));

                string userIdString = "," + userId + ",";
                Meeting meeting = new Meeting()
                {
                    Date = bookedMeetingVm.Date,
                    StartTime = bookedMeetingVm.StartTime,
                    EndTime = bookedMeetingVm.EndTime,
                    MeetingRoomId = bookedMeetingVm.MeetingRoomId,
                    CurrentlyApproval = userIdString,
                    Order = 1
                };
                AuditInsert(meeting);
                var timeSlot = _meetingTimeSlotManager.GetTimeSlotIdsByTimeRange(bookedMeetingVm.StartTime, bookedMeetingVm.EndTime);
                if (_meetingManager.Add(meeting))
                {
                    var meetingId = meeting.Id;
                    BookedMeetingRoom bookedMeetingRoom = new BookedMeetingRoom()
                    {
                       TimeSlot = timeSlot,
                       MeetingId = meetingId

                    };                   
                    if (_bookedMeetingRoomManager.Add(bookedMeetingRoom))
                    {
                        //commit
                        return OkResult(meeting,"Meeting booked successfully.");
                    }
                }
                //roleback
                return BadRequest("Meeting booked failed.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException.Message);
            }
        }
        private List<int> SaveClient(List<Client> client)
        {
                List<int> retunList = new List<int>();
                foreach (var item in client)
                {
                    var checkClient = _externalUserManager.GetByPhoneNo(item.Phone);
                    if (checkClient != null)
                    {
                        retunList.Add(item.Id);

                    }
                    else
                    {
                        Client externalUser = new Client()
                        {
                            Name = item.Name,
                            CompanyName = item.CompanyName,
                            Email = item.Email,
                            Phone = item.Phone
                        };
                        AuditInsert(externalUser);
                        _externalUserManager.Add(externalUser);

                        retunList.Add(externalUser.Id);
                    }

                }
                return retunList;
        }

        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult SaveMeeting(SaveMeetingVm saveMeetingVm)
        {

            try
            {
                var returnList = SaveClient(saveMeetingVm.Clients);
                foreach (var item in returnList)
                {
                    Participant p = new Participant();
                    p.MeetingId = saveMeetingVm.MeetingId;
                    p.ReferenceId = item;
                    p.IsUser = false;
                    saveMeetingVm.Participants.Add(p);
                }



                var meeting = _meetingManager.GetById(saveMeetingVm.MeetingId);
                meeting.IsSave = true;
                if (_meetingManager.Update(meeting))
                {
                    if (_participantManager.Add(saveMeetingVm.Participants))
                    {
                        //commit

                        return Ok("Meeting saved successfully.");

                    };
                }
                //roleback
                return BadRequest("Meeting not saved");


            }
            catch (Exception ex)
            {   //roleback
                return BadRequest(ex.Message);
            }
        }
    }
}
