using Base.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserManagement.Data;
using UserManagement.Interface;
using UserManagement.Manager;
using UserManagement.Models;
using UserManagement.ViewModels;

namespace UserManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentManager _departmentManager;
        public DepartmentController(AppDbContext dbContext)
        {
            _departmentManager = new DepartmentManager(dbContext);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Department department)
        {
            try
            {
                AuditInsert(department);
                if(_departmentManager.Add(department))
                {
                    return  Ok(department);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            try
            {
                string sql = "Select * From Departments where IsActive=1";
                var data = _departmentManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new Department()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = dr["Name"].ToString(),
                               
                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(Department department)
        {
            try
            {
                var existingData = _departmentManager.GetById(department.Id);
                if (existingData == null)
                {
                    return NotFound();
                }
                existingData.Name = department.Name;   
                AuditUpdate(existingData);
                bool isUpdate = _departmentManager.Update(existingData);
                if (isUpdate)
                {
                    return Ok("Department updated successfully.");
                }
                else
                {
                    return BadRequest($"{existingData.Name} not found");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var data = _departmentManager.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            AuditDelete(data);
            bool isDelete = _departmentManager.Update(data);
            if (isDelete)
            {
                return Ok("Department deleted successfully.");
            }
            else
            {
                return BadRequest($"{data.Name} not found");
            }
        }

    }
    
}
