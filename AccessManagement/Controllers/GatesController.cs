using AccessManagement.Data;
using AccessManagement.Interface;
using AccessManagement.Manager;
using AccessManagement.Models;
using Base.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using System.Data;

namespace AccessManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class GatesController : BaseController
    {
        private readonly IGateManager _gateManager;
        public GatesController(ApplicationDbContext db)
        {
            _gateManager = new GateManager(db);
        }
        [HttpPost]
        public IActionResult Add(Gates gates)
        {
            try
            {
                if(gates==null)
                {
                    return BadRequest("Nothing to save.");
                }
                AuditInsert(gates);
                if(_gateManager.Add(gates))
                {
                    return Ok("Data Save Successfully.");
                }else
                {
                    return BadRequest();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut]
        public IActionResult Edit(Gates gates)
        {
            try
            {
                var gate = _gateManager.GetById(gates.Id);
                if (gate == null)
                {
                    return NotFound();
                }
                gate.ControllerSn = gates.ControllerSn;
                gate.Ip = gates.Ip;
                gate.DoorNo = gates.DoorNo;
                gate.MeetingRoomId = gates.MeetingRoomId;
                AuditUpdate(gate);
                bool isUpdate = _gateManager.Update(gate);
                if (isUpdate)
                {
                    return Ok("Update Successfully.");
                }
                else
                {
                    return BadRequest();
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpDelete] 
        public IActionResult Delete(int id)
        { 
            var gate= _gateManager.GetById(id);
            if (gate == null)
            {
                return NotFound();
            }
            AuditDelete(gate);
            bool isDeleted= _gateManager.Delete(gate);
            if(isDeleted)
            {
                return Ok("Data successfully deleted.");
            }
            else
            {
                return BadRequest("failed deleted.");
            }
        }
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                string sql = "Select * From Gates where IsActive=1";
                var data = _gateManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new Gates()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                ControllerSn = dr["ControllerSn"].ToString(),
                                Ip = dr["Ip"].ToString(),
                                DoorNo = dr["DoorNo"].ToString(),
                                MeetingRoomId = Convert.ToInt32(dr["MeetingRoomId"]),
                                


                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _gateManager.GetById(id);
                if (data == null)
                {
                    return BadRequest("Data not found.");
                }
                return Ok(data);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
