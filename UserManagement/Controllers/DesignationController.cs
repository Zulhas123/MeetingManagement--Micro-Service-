using Base.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;
using UserManagement.Data;
using UserManagement.Interface;
using UserManagement.Manager;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class DesignationController : BaseController
    {
        private readonly IDesignationManager _designationManager;
        public DesignationController(AppDbContext db) 
        {
            _designationManager = new DesignationManager(db);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Designation designation)
        {
            try
            {
                AuditInsert(designation);
                if (_designationManager.Add(designation))
                {
                    return Ok(designation);
                }
                return BadRequest();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDesignation()
        {
            try
            {
                string query = "Select * From Designations Where IsActive=1";
                var data=_designationManager.ExecuteRawSql(query);
                var list = (from DataRow dtRow in data.Rows
                            select new Designation()
                            {
                                Id = Convert.ToInt32(dtRow["Id"]),
                                Name = dtRow["Name"].ToString(),
                                DesignationOrder = Convert.ToInt32(dtRow["DesignationOrder"])
                            }).ToList();
                return Ok(list);
                          
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDesignation(Designation designation)
        {
            var data = _designationManager.GetById(designation.Id);
            if (data == null)
            {
                return NotFound();
            }
            data.Name = designation.Name;
            data.DesignationOrder = designation.DesignationOrder;
            AuditUpdate(designation);
           bool isUpdated= _designationManager.Update(designation);
            if (isUpdated)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest($"{data.Name} not found");
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var data=_designationManager.GetById(id);
            if (data == null)
            {
                return NotFound();
            }AuditDelete(data);
            bool isDeleted = _designationManager.Update(data);
            if (isDeleted)
            {
                return Ok("Designation deleted successfully.");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
