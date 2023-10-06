using Base.API.Controllers;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using MeetingManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class MeetingRoomController : BaseController
    {
        private readonly IMeetingRoomManager _meetingRoomManager;
        private readonly IMeetingManager _meetingManager;
        private readonly IMeetingTimeSlotManager _meetingTimeSlotManager;
        private readonly IBookedMeetingRoomManager _bookedMeetingRoomManager;
        private readonly ApplicationDbContext db;
        public MeetingRoomController(ApplicationDbContext dbContext)
        {
            _meetingRoomManager = new MeetingRoomManager(dbContext);
            _meetingManager = new MeetingManager(dbContext);
            _meetingTimeSlotManager = new MeetingTimeSlotManager(dbContext);
            _bookedMeetingRoomManager = new BookedMeetingRoomManager(dbContext);
            db = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MeetingRoom meetingRoom)
        {
            try
            {
                AuditInsert(meetingRoom);
                if (_meetingRoomManager.Add(meetingRoom))
                {
                    return Ok(meetingRoom);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string sql = "Select * From MeetingRooms where IsActive=1";
                var data = _meetingRoomManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new MeetingRoom()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                RoomNo = Convert.ToInt32(dr["RoomNo"]),
                                FloorNo = Convert.ToInt32(dr["FloorNo"]),
                                Capacity = Convert.ToInt32(dr["Capacity"]),
                                MeetingRoomTypeId = Convert.ToInt32(dr["MeetingRoomTypeId"]),
                                Features = dr["Features"].ToString(),


                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(MeetingRoom meetingRoom)
        {
            try
            {
                var existingData = _meetingRoomManager.GetById(meetingRoom.Id);
                if (existingData == null)
                {
                    return NotFound();
                }
                existingData.RoomNo = meetingRoom.RoomNo;
                existingData.FloorNo = meetingRoom.FloorNo;
                existingData.Capacity = meetingRoom.Capacity;
                existingData.MeetingRoomTypeId = meetingRoom.MeetingRoomTypeId;
                existingData.Features = meetingRoom.Features;
                AuditUpdate(existingData);
                bool isUpdate = _meetingRoomManager.Update(existingData);
                if (isUpdate)
                {
                    return Ok(existingData);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = _meetingRoomManager.GetById(id);
                if (data == null)
                {
                    return NotFound();
                }
                AuditDelete(data);
                bool isDelete = _meetingRoomManager.Update(data);
                if (isDelete)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _meetingRoomManager.GetById(id);
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
        [HttpGet]
        public IActionResult GetAvailableRoom(DateTime date, TimeSpan? startSlot, TimeSpan? endSlot, int? capacity,int? roomType)
        {
            try
            {
               var meetingDate = date.ToString("yyyy-MM-dd");
                string query = $"SELECT m.Id AS MeetingId, m.MeetingRoomId, bm.TimeSlot AS TimeSlotId INTO #BookedSlot FROM Meetings m JOIN BookedMeetingRooms bm ON m.Id = bm.MeetingId WHERE m.Date = '{meetingDate}' SELECT  mr.Id AS MeetingRoomId,mr.RoomNo,mr.FloorNo, mr.Capacity, mr.Features, mts.SlotStart AS TimeSlotStart, mts.SlotEnd AS TimeSlotEnd, mr.MeetingRoomTypeId FROM MeetingRooms mr CROSS JOIN MeetingTimeSlots mts LEFT JOIN #BookedSlot bs ON mr.Id = bs.MeetingRoomId AND CHARINDEX(CAST(mts.Id AS NVARCHAR), bs.TimeSlotId)>0 WHERE mr.IsActive = 1   AND mts.IsActive = 1";
                if (startSlot != null && endSlot!=null)
                {
                    query += $" AND mts.SlotStart >= '{startSlot}'  AND mts.SlotEnd <= '{endSlot}' ";
                }
                if (capacity != null)
                {
                    query += $"AND mr.Capacity>={capacity}";
                }
                if(roomType != null)
                {
                    query += $" AND mr.MeetingRoomTypeId = {roomType}";
                }

                 query+= $" AND bs.TimeSlotId IS NULL Order By MeetingRoomId DROP TABLE #BookedSlot";
                //var stringQuery = $"SELECT m.Id AS MeetingId, m.MeetingRoomId, bm.TimeSlot AS TimeSlotId INTO #BookedSlot FROM Meetings m JOIN BookedMeetingRooms bm ON m.Id = bm.MeetingId WHERE m.Date = '{meetingDate}' SELECT  mr.Id AS MeetingRoomId,mr.RoomNo,mr.FloorNo, mr.Capacity, mr.Features, mts.SlotStart AS TimeSlotStart, mts.SlotEnd AS TimeSlotEnd FROM MeetingRooms mr CROSS JOIN MeetingTimeSlots mts LEFT JOIN #BookedSlot bs ON mr.Id = bs.MeetingRoomId AND CHARINDEX(CAST(mts.Id AS NVARCHAR), bs.TimeSlotId)>0 WHERE mr.IsActive = 1   AND mts.IsActive = 1 AND mts.SlotStart >= '{startSlot}'  AND mts.SlotEnd <= '{endSlot}' AND bs.TimeSlotId IS NULL Order By MeetingRoomId DROP TABLE #BookedSlot";
                var dataTable = _meetingRoomManager.ExecuteRawSql(query);


                var data = (from DataRow dr in dataTable.Rows
                            select new AvailableRoomsInfoVm()
                            {
                                Capacity = Convert.ToInt32(dr["Capacity"]),
                                Feature = dr["Features"].ToString(),
                                FloorNo = Convert.ToInt32(dr["FloorNo"]),
                                MeetingRoomId = Convert.ToInt32(dr["MeetingRoomId"]),
                                RoomNo = Convert.ToInt32(dr["RoomNo"]),
                                TimeSlotEnd = TimeSpan.Parse(dr["TimeSlotEnd"].ToString()),
                                TimeSlotStart = TimeSpan.Parse(dr["TimeSlotStart"].ToString()),
                            }).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
