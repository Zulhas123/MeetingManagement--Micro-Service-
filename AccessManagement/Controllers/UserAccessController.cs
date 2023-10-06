using AccessManagement.Data;
using AccessManagement.Interface;
using AccessManagement.Manager;
using AccessManagement.Models;
using Base.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace AccessManagement.Controllers
{
    [ApiController,Route("[controller]/[action]")]
    public class UserAccessController : BaseController
    {
        private readonly IUserAccessManager _userAccessManager;
        private readonly IGateManager _gateManager;
        public UserAccessController(ApplicationDbContext db)
        {
            _userAccessManager = new UserAccessManager(db);
            _gateManager= new GateManager(db);
        }
        [HttpPost]
        public IActionResult Add(UserAccess userAccess)
        {
            try
            {
                if (userAccess == null)
                {
                    return BadRequest("nothing to save.");
                }
                if(_userAccessManager.Add(userAccess))
                {
                    return Ok("Data save successfully.");
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
        //[HttpPost]
        //public IActionResult CheckAccess(UserAccess userAccess)
        //{
        //  var check=_userAccessManager.CheckAccessUser(userAccess.CardNo);



        //    if (check != null &&
        //        check.StartAt <= DateTime.Now.TimeOfDay &&
        //        check.EndAt >= DateTime.Now.TimeOfDay)
        //    {

        //       return Ok("The user has valid access.");
        //    }
        //    else
        //    {

        //        return BadRequest("The user does not have valid access.");
        //    }

        //}
        [HttpGet]
        public IActionResult CheckAccess(string cardNo, string controllerSn, string doorNo)
        {
            HasAccessVm res = new HasAccessVm();
            
            var gate = _gateManager.GetByControllerAndDoorNo(controllerSn, doorNo);
            if (gate != null)
            {
                var Checkcard = _userAccessManager.CheckAccessUser(cardNo);

                if (Checkcard != null && Checkcard.StartAt <= DateTime.Now.TimeOfDay &&
                             Checkcard.EndAt >= DateTime.Now.TimeOfDay)
                {

                    res = new HasAccessVm
                    {
                        HasAccess = true,
                        Ip = gate.Ip
                    };
                    UserAccess obj = new UserAccess()
                    {
                     
                        CardNo = cardNo,
                        
                    };
                    _userAccessManager.Add(obj);

                    return OkResult(res);

                }


            }
            return OkResult(res);

        }
    }
}
