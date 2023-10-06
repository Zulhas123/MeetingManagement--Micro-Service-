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
    public class MeetingTimeSlotController : BaseController
    {
        private readonly IMeetingTimeSlotManager meetingTimeSlotManager;

        public MeetingTimeSlotController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            meetingTimeSlotManager = new MeetingTimeSlotManager(dbContext);
        }
        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Add(MeetingTimeSlot meetingTimeSlot)
        {
            try
            {
                AuditInsert(meetingTimeSlot);
                var result = meetingTimeSlotManager.Add(meetingTimeSlot);
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

        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Update(MeetingTimeSlot meetingTimeSlot)
        {
            try
            {
                var oldData = meetingTimeSlotManager.GetById(meetingTimeSlot.Id);
                if (oldData == null)
                {
                    return NotFound("Data not found");
                }
                oldData.SlotStart = meetingTimeSlot.SlotStart;
                AuditUpdate(oldData);
                var result = meetingTimeSlotManager.Update(oldData);
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
                var Data = meetingTimeSlotManager.GetById(Id);
                if (Data == null)
                {
                    return NotFound("Data not found");
                }
                AuditDelete(Data);
                var result = meetingTimeSlotManager.Delete(Data);
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
                var sql = "Select * From MeetingTimeSlots where IsActive=1";
                var Data = meetingTimeSlotManager.ExecuteRawSql(sql);
                List<MeetingTimeSlot> MeetingTimeSlotList = new List<MeetingTimeSlot>();
                MeetingTimeSlotList = (from DataRow dr in Data.Rows
                                            select new MeetingTimeSlot()
                                            {
                                                Id = Convert.ToInt32(dr["Id"]),
                                                SlotStart = TimeSpan.Parse(dr["SlotStart"].ToString()),
                                                SlotEnd = TimeSpan.Parse(dr["SlotEnd"].ToString()),
                                                IsActive = Convert.ToBoolean(dr["IsActive"])
                                            }).ToList();
                return Ok(MeetingTimeSlotList);
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }
    }
}

