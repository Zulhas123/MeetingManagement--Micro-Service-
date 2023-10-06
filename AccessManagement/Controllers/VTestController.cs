using Base.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class VTestController : BaseController
    {
        public VTestController() { }   

        public IActionResult Get()
        {
            return Ok();    
        }
    }
}
