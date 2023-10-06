using Base.API.Controllers;
using Base.API.Models;
using Base.API.SecurityExtension;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Data;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    
    public class MeetingSummeryController : BaseController
    {
        private readonly ImeetingSummeryManager meetingSummeryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MeetingSummeryController(ApplicationDbContext dbContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            meetingSummeryManager = new MeetingSummeryManager(dbContext);
            _webHostEnvironment= webHostEnvironment;
        }
        
        [HttpPost]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult AddMeetingSummary(MeetingSummery meetingSummery, IFormFile file) // Need to Check it is ok or not
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                meetingSummery.File = uniqueFileName;
                var result = meetingSummeryManager.Add(meetingSummery);

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    // Delete the uploaded file if saving fails
                    System.IO.File.Delete(filePath);
                    return BadRequest("Data not saved.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult GetListByMeetingId(int meetingId)
        {
            try
            {
                string sql = $"SELECT * FROM MeetingSummery WHERE IsActive = 1 AND MeetingId = {meetingId}";
                var data = meetingSummeryManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new MeetingSummery()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                MeetingId = Convert.ToInt32(dr["MeetingId"]),
                                File = dr["File"].ToString(),
                                SummeryDetails = dr["SummeryDetails"].ToString(),
                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = meetingSummeryManager.GetById(id);
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
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Update(int id)
        {
            try
            {
                var existData = meetingSummeryManager.GetById(id);
                if (existData == null)
                {
                    return NotFound("Data not found");
                }                
                var result = meetingSummeryManager.Update(existData);
                if (result)
                {
                    return Ok(existData);
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
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Delete(int Id)
        {
            try
            {
                var Data = meetingSummeryManager.GetById(Id);
                if (Data == null)
                {
                    return NotFound("Data not found");
                }
               
                var result = meetingSummeryManager.Delete(Data);
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
    }
}
