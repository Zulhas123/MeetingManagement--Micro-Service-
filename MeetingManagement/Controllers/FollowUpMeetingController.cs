using Base.API.Controllers;
using Base.API.SecurityExtension;
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
    public class FollowUpMeetingController : BaseController
    {
        private readonly IFollowUpMeetingManager followUpMeetingManager;
        public FollowUpMeetingController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            followUpMeetingManager = new FollowUpMeetingManager(dbContext);
        }

        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public async Task<IActionResult> Add([FromBody] FollowUpMeeting followUpMeeting)
        {
            try
            {
                AuditInsert(followUpMeeting);
                if (followUpMeetingManager.Add(followUpMeeting))
                {
                    return Ok(followUpMeeting);
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

        [HttpPut]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Update(FollowUpMeeting followUpMeeting)
        {
            try
            {
                var oldData = followUpMeetingManager.GetById(followUpMeeting.Id);
                if (oldData == null)
                {
                    return NotFound("Data not found");
                }
                oldData.UserId = followUpMeeting.UserId;
                oldData.MettingId = followUpMeeting.MettingId;
                AuditUpdate(oldData);
                var result = followUpMeetingManager.Update(oldData);
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
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Delete(int Id)
        {
            try
            {
                var Data = followUpMeetingManager.GetById(Id);
                if (Data == null)
                {
                    return NotFound("Data not found");
                }
                AuditDelete(Data);
                var result = followUpMeetingManager.Update(Data);
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
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult GetfollowUpByMeetingById(int id)
        {
            try
            {
                var data = followUpMeetingManager.GetById(id);
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
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult GetList()
        {
            try
            {
                var sql = "Select * from FollowUpMeeting where IsActive=1";
                var Data = followUpMeetingManager.ExecuteRawSql(sql);
                List<FollowUpMeeting> followUpMeetingList = new List<FollowUpMeeting>();
                followUpMeetingList = (from DataRow dr in Data.Rows
                                    select new FollowUpMeeting()
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        UserId = Convert.ToInt32(dr["UserId"]),
                                        MettingId = Convert.ToInt32(dr["MettingId"]),
                                    }).ToList();
                return Ok(followUpMeetingList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult GetListByMeetingId(int MettingId)
        {
            try
            {
                var sql = $"SELECT * FROM FollowUpMeeting WHERE MettingId = {MettingId}";
                var Data = followUpMeetingManager.ExecuteRawSql(sql);
                List<FollowUpMeeting> followUpMeetingList = new List<FollowUpMeeting>();
                followUpMeetingList = (from DataRow dr in Data.Rows
                                       select new FollowUpMeeting()
                                       {
                                           Id = Convert.ToInt32(dr["Id"]),
                                           UserId = Convert.ToInt32(dr["UserId"]),
                                           MettingId = Convert.ToInt32(dr["MettingId"]),
                                       }).ToList();
                return Ok(followUpMeetingList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
