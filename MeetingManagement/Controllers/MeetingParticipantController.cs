using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class MeetingParticipantController : ControllerBase
    {
        private readonly IParticipantManager participantManager;
        private readonly IMeetingManager _meetingManager;
        public MeetingParticipantController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            participantManager = new ParticipantManager(dbContext);
            _meetingManager = new MeetingManager(dbContext);
        }


        [HttpPost]
        public IActionResult Add(Participant participant)
        {
            try
            {
                //FullAudit(client);

                if (participantManager.Add(participant))
                {
                    return Ok(participant);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Update(Participant participant)
        {
            try
            {
                var oldData = participantManager.GetById(participant.Id);
                if (oldData == null)
                {
                    return NotFound("Data not found");
                }
                //AuditUpdate(oldData);
                var result = participantManager.Update(oldData);
                if (result)
                {
                    return Ok(oldData);
                }
                else
                {
                    return BadRequest("Data not update");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            try
            {
                var Data = participantManager.GetById(Id);
                if (Data == null)
                {
                    return NotFound("Data not found");
                }
                //AuditDelete(Data);
                var result = participantManager.Delete(Data);
                if (result)
                {
                    return Ok("Delete Successfully");
                }
                else
                {
                    return BadRequest("Delete Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = participantManager.GetById(id);
                if (data == null)
                {
                    return BadRequest("Data not found !!");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        //[MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult GetList()
        {
            try
            {
                var sql = "SELECT * FROM Participants WHERE IsActive = 1";
                var Data = participantManager.ExecuteRawSql(sql);
                List<Participant> ParticipantList = new List<Participant>();
                ParticipantList = (from DataRow dr in Data.Rows
                                   select new Participant()
                                   {
                                       Id = Convert.ToInt32(dr["Id"]),
                                       MeetingId = Convert.ToInt32(dr["MeetingId"]),
                                       ReferenceId = Convert.ToInt32(dr["ReferenceId"]),
                                       IsUser = Convert.ToBoolean(dr["IsUser"]),
                                       IsConfirmed = Convert.ToBoolean(dr["IsConfirmed"]),
                                       IsRejected = Convert.ToBoolean(dr["IsRejected"]),
                                       RejectNote = dr["RejectNote"].ToString()
                                   }).ToList();

                return Ok(ParticipantList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public IActionResult GetListByMeetingId(int meetingId)
        {
            try
            {
                string sql = $"SELECT * FROM Participants WHERE isActive = 1 AND MeetingId = {meetingId}";
                var Data = participantManager.ExecuteRawSql(sql);
                List<Participant> ParticipantList = new List<Participant>();
                ParticipantList = (from DataRow dr in Data.Rows
                                   select new Participant()
                                   {
                                       Id = Convert.ToInt32(dr["Id"]),
                                       MeetingId = Convert.ToInt32(dr["MeetingId"]),
                                       ReferenceId = Convert.ToInt32(dr["ReferenceId"]),
                                       IsUser = Convert.ToBoolean(dr["IsUser"]),
                                       IsConfirmed = Convert.ToBoolean(dr["IsConfirmed"]),
                                       IsRejected = Convert.ToBoolean(dr["IsRejected"]),
                                       RejectNote = dr["RejectNote"].ToString()
                                   }).ToList();

                return Ok(ParticipantList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult RemoveFromMeetingById(int meetingId)
        {
            try
            {
                var meeting = _meetingManager.GetById(meetingId);
                if (meeting == null)
                {
                    return NotFound("Data not found");
                }
                var result = _meetingManager.Delete(meeting);
                if (result)
                {
                    return Ok("Delete Successful");
                }
                else
                {
                    return NotFound("Meeting not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
