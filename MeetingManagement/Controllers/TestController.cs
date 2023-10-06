using Base.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using MeetingManagement.Data;
using MeetingManagement.Manager;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class TestController : BaseController
    {
        private readonly ApplicationDbContext _dbContext;

        public TestController(ApplicationDbContext db)
        {
            _dbContext= db;

        }
        public TestController() { }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }
    }
}
