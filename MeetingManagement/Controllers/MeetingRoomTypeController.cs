using Base.API.Controllers;
using Base.API.SecurityExtension;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class MeetingRoomTypeController : BaseController
    {
        private readonly IMeetingRoomTypeManager meetingRoomTypeManager;

        public MeetingRoomTypeController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            meetingRoomTypeManager = new MeetingRoomTypeManager(dbContext);
        }
        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Add(MeetingRoomType meetingRoomType)
        {
            try
            {
                AuditInsert(meetingRoomType);
                var result = meetingRoomTypeManager.Add(meetingRoomType);
                if (result)
                {
                    return OkResult(result);
                }
                else
                {
                    return BadRequestResult("Data not saved");
                }
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }

        [HttpPut]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Update(MeetingRoomType meetingRoomType)
        {
            try
            {
                var oldData = meetingRoomTypeManager.GetById(meetingRoomType.Id);
                if (oldData == null)
                {
                    return NotFound("Data not found");
                }
                oldData.Name = meetingRoomType.Name;
                AuditUpdate(oldData);
                var result = meetingRoomTypeManager.Update(oldData);
                if (result)
                {
                    return OkResult(oldData);
                }
                else
                {
                    return BadRequestResult("Data not update");
                }
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }
        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Delete(int Id)
        {
            try
            {
                var Data = meetingRoomTypeManager.GetById(Id);
                if (Data == null)
                {
                    return NotFound("Data not found");
                }
                AuditDelete(Data);
                var result = meetingRoomTypeManager.Delete(Data);
                if (result)
                {
                    return OkResult("Delete Successfully");
                }
                else
                {
                    return BadRequestResult("Delete Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }

        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult List()
        {
            try
            {
                var sql = "Select * From MeetingRoomTypes where IsActive=1";
                var Data = meetingRoomTypeManager.ExecuteRawSql(sql);
                List<MeetingRoomType> MeetingRoomTypeList = new List<MeetingRoomType>();
                MeetingRoomTypeList = (from DataRow dr in Data.Rows
                                       select new MeetingRoomType()
                                       {
                                           Id = Convert.ToInt32(dr["Id"]),
                                           Name = dr["Name"].ToString(),
                                       }).ToList();
                return Ok(MeetingRoomTypeList);
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = meetingRoomTypeManager.GetById(id);
                if (data == null)
                {
                    return BadRequest("Data not found.please try again.");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
    }
}
