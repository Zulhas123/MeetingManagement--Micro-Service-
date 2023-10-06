using Base.API.Controllers;
using Base.API.SecurityExtension;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using MeetingManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class MeetingApprovalLayerController : BaseController
    {
        private readonly IMeetingApprovalLayerManager meetingApprovalLayerManager;
        private readonly IMeetingManager _meetingManager;
        public MeetingApprovalLayerController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            meetingApprovalLayerManager = new MeetingApprovalLayerManager(dbContext);
            _meetingManager = new MeetingManager(dbContext);
        }
        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Add(MeetingApprovalLayer meetingApprovalLayer)
        {
            try
            {
                AuditInsert(meetingApprovalLayer);
                var result = meetingApprovalLayerManager.Add(meetingApprovalLayer);
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
        public IActionResult Update(MeetingApprovalLayer meetingApprovalLayer)
        {
            try
            {
                var oldData = meetingApprovalLayerManager.GetById(meetingApprovalLayer.Id);
                if (oldData == null)
                {
                    return NotFound("Data not found");
                }
                oldData.UserId = meetingApprovalLayer.UserId;
                oldData.Order = meetingApprovalLayer.Order;
                AuditUpdate(oldData);
                var result = meetingApprovalLayerManager.Update(oldData);
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
                var Data = meetingApprovalLayerManager.GetById(Id);
                if (Data == null)
                {
                    return NotFound("Data not found");
                }
                AuditDelete(Data);
                var result = meetingApprovalLayerManager.Update(Data);
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
        public IActionResult GetList()
        {
            try
            {
                var sql = "Select * From MeetingApprovalLayers where IsActive=1";
                var Data = meetingApprovalLayerManager.ExecuteRawSql(sql);
                List<MeetingApprovalLayer> MeetingApprovalLayerList = new List<MeetingApprovalLayer>();
                MeetingApprovalLayerList = (from DataRow dr in Data.Rows
                                       select new MeetingApprovalLayer()
                                       {
                                           Id = Convert.ToInt32(dr["Id"]),
                                           UserId = Convert.ToInt32(dr["UserId"]),
                                           Order = Convert.ToInt32(dr["Order"]),
                                       }).ToList();
                return Ok(MeetingApprovalLayerList);
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }              
    }
}

