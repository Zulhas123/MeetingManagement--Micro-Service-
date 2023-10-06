using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using Base.API.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Base.API.Models;
using System.Data;
using System.Numerics;
using Base.API.SecurityExtension;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class ExternalUserController : BaseController
    {
        private readonly IExternalUserManager externalUserManager;

        public ExternalUserController(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            externalUserManager = new ExternalUserManager(dbContext);
        }
        [HttpPut]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Update(Client client)
        {
            try
            {
                var oldData = externalUserManager.GetById(client.Id);
                if (oldData == null)
                {
                    return NotFound("Data not found");
                }
                oldData.Name= client.Name;
                oldData.CompanyName = client.CompanyName;
                oldData.Email= client.Email;
                oldData.Phone= client.Phone;
                AuditUpdate(oldData);
                var result = externalUserManager.Update(oldData);
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
                var Data = externalUserManager.GetById(Id);
                if (Data == null)
                {
                    return NotFound("Data not found");
                }
                AuditDelete(Data);
                var result = externalUserManager.Update(Data);
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
        public IActionResult GetByExternalUser(int id)
        {
            try
            {
                var data = externalUserManager.GetById(id);
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
                var sql = "Select * from ExternalUsers where IsActive=1";
                var Data = externalUserManager.ExecuteRawSql(sql);
                List<Client> externalUserList = new List<Client>();
                externalUserList = (from DataRow dr in Data.Rows
                                       select new Client()
                                       {
                                           Id = Convert.ToInt32(dr["Id"]),
                                           Name = dr["Name"].ToString(),
                                           CompanyName = dr["CompanyName"].ToString(),
                                           Email = dr["Email"].ToString(),
                                           Phone = dr["Phone"].ToString()
                                       }).ToList();
                return Ok(externalUserList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult GetbyMobileNo(string phoneNo)
        {
            try
            {
                var data = externalUserManager.GetByPhoneNo(phoneNo);
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
    }
}
