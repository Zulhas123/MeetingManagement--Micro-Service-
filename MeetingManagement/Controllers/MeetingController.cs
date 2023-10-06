using Base.API.Controllers;
using Base.API.Models;
using Base.API.SecurityExtension;
using MeetingManagement.Data;
using MeetingManagement.Interface;
using MeetingManagement.Manager;
using MeetingManagement.Models;
using MeetingManagement.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;

namespace MeetingManagement.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class MeetingController : BaseController
    {
        private readonly IMeetingManager _meetingManager;
        private readonly MeetingApprovalLayerManager _meetingApprovalLayerManager;
        private readonly ParticipantManager _participantManager;
        private readonly IBookedMeetingRoomManager bookedMeetingRoomManager;

        public MeetingController(ApplicationDbContext dbContext)
        {
            _meetingManager = new MeetingManager(dbContext);
            _meetingApprovalLayerManager = new MeetingApprovalLayerManager(dbContext);
            bookedMeetingRoomManager = new BookedMeetingRoomManager(dbContext);
            _participantManager = new ParticipantManager(dbContext);
        }
        [HttpPut]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult Update(Meeting meeting)
        {
            try
            {
                var existingData = _meetingManager.GetById(meeting.Id);
                if (existingData == null)
                {
                    return NotFound();
                }
                existingData.MeetingRoomId = meeting.Id;
                existingData.Date = meeting.Date;
                existingData.StartTime = meeting.StartTime;
                existingData.EndTime = meeting.EndTime;
                existingData.IsApproved = meeting.IsApproved;
                existingData.CurrentlyApproval = meeting.CurrentlyApproval;
                existingData.AlreadyApproved = meeting.AlreadyApproved;
                existingData.Order = meeting.Order;
                existingData.IsReject = meeting.IsReject;
                existingData.IsSave = meeting.IsSave;
                AuditUpdate(existingData);
                bool isUpdate = _meetingManager.Update(existingData);
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
        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                string sql = "Select * From Meetings where IsActive=1";
                var data = _meetingManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new Meeting()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                MeetingRoomId = Convert.ToInt32(dr["MeetingRoomId"]),
                                Date = Convert.ToDateTime(dr["Date"]),
                                StartTime = TimeSpan.Parse(dr["StartTime"].ToString()),
                                EndTime = TimeSpan.Parse(dr["EndTime"].ToString()),
                                IsApproved = Convert.ToBoolean(dr["IsApproved"]),
                                CurrentlyApproval = dr["CurrentlyApproval"].ToString(),
                                AlreadyApproved = dr["AlreadyApproved"].ToString(),
                                Order = Convert.ToInt32(dr["Order"]),
                                IsReject = Convert.ToBoolean(dr["IsReject"]),
                                IsSave = Convert.ToBoolean(dr["IsSave"]),
                                RejectNote = dr["RejectNote"].ToString(),
                                RejectBy = Convert.ToInt32(dr["RejectBy"]),
                                RejectDate = Convert.ToDateTime(dr["RejectDate"]),


                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = _meetingManager.GetById(id);
                if (data == null)
                {
                    return NotFound();
                }
                AuditDelete(data);
                bool isDelete = _meetingManager.Delete(data);
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
                var data = _meetingManager.GetById(id);
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
        public async Task<IActionResult> GetListByUser(int userId)
        {
            try
            {
                //var userId=
                string sql = $"Select * From Meetings where IsActive=1 and CreatedBy={userId}";
                var data = _meetingManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new Meeting()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                MeetingRoomId = Convert.ToInt32(dr["MeetingRoomId"]),
                                Date = Convert.ToDateTime(dr["Date"]),
                                StartTime = TimeSpan.Parse(dr["StartTime"].ToString()),
                                EndTime = TimeSpan.Parse(dr["EndTime"].ToString()),
                                IsApproved = Convert.ToBoolean(dr["IsApproved"]),
                                CurrentlyApproval = dr["CurrentlyApproval"].ToString(),
                                AlreadyApproved = dr["AlreadyApproved"].ToString(),
                                Order = Convert.ToInt32(dr["Order"]),
                                IsReject = Convert.ToBoolean(dr["IsReject"]),
                                IsSave = Convert.ToBoolean(dr["IsSave"]),
                                RejectNote = dr["RejectNote"].ToString(),
                                RejectBy = Convert.ToInt32(dr["RejectBy"]),
                                RejectDate = Convert.ToDateTime(dr["RejectDate"]),


                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetListByRoom(int roomId)
        {
            try
            {
                string sql = $"Select * From Meetings where IsActive=1 AND MeetingRoomId = {roomId}";
                var data = _meetingManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new Meeting()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                MeetingRoomId = Convert.ToInt32(dr["MeetingRoomId"]),
                                Date = Convert.ToDateTime(dr["Date"]),
                                StartTime = TimeSpan.Parse(dr["StartTime"].ToString()),
                                EndTime = TimeSpan.Parse(dr["EndTime"].ToString()),
                                IsApproved = Convert.ToBoolean(dr["IsApproved"]),
                                CurrentlyApproval = dr["CurrentlyApproval"].ToString(),
                                AlreadyApproved = dr["AlreadyApproved"].ToString(),
                                Order = Convert.ToInt32(dr["Order"]),
                                IsReject = Convert.ToBoolean(dr["IsReject"]),
                                IsSave = Convert.ToBoolean(dr["IsSave"]),
                                RejectNote = dr["RejectNote"].ToString(),
                                RejectBy = Convert.ToInt32(dr["RejectBy"]),
                                RejectDate = Convert.ToDateTime(dr["RejectDate"]),


                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetListByTimeSlot(TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                string sql = $"Select * From Meetings where IsActive=1  AND StartTime <= '{startTime}' AND EndTime >= '{endTime}'";
               // string sql = $"SELECT CONVERT(TIME, StartTime) AS StartTime, CONVERT(TIME, EndTime) AS EndTime FROM Meetings WHERE IsActive = 1 AND StartTime <= '{startTime}' AND EndTime >= '{endTime}'";
                var data = _meetingManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new Meeting()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                MeetingRoomId = Convert.ToInt32(dr["MeetingRoomId"]),
                                Date = Convert.ToDateTime(dr["Date"]),
                                StartTime = TimeSpan.Parse(dr["StartTime"].ToString()),
                                EndTime = TimeSpan.Parse(dr["EndTime"].ToString()),
                                IsApproved = Convert.ToBoolean(dr["IsApproved"]),
                                CurrentlyApproval = dr["CurrentlyApproval"].ToString(),
                                AlreadyApproved = dr["AlreadyApproved"].ToString(),
                                Order = Convert.ToInt32(dr["Order"]),
                                IsReject = Convert.ToBoolean(dr["IsReject"]),
                                IsSave = Convert.ToBoolean(dr["IsSave"]),
                                RejectNote = dr["RejectNote"].ToString(),
                                RejectBy = Convert.ToInt32(dr["RejectBy"]),
                                RejectDate = Convert.ToDateTime(dr["RejectDate"])
                            });

                var getlist = _meetingManager.GetListByTimeRange(startTime, endTime);
                if (getlist.Count == 0)
                {
                    return BadRequest("Meeting Is Not Found Between The Time Slot !!");
                }
                else
                {
                    return Ok(getlist);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetListByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var getlist = _meetingManager.GetListByDateRange(startDate, endDate);
                if (getlist.Count == 0)
                {
                    return BadRequest("Data is not found !!");
                }
                else
                {
                    return Ok(getlist);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {

                var meeting = _meetingManager.GetById(id);
                if (meeting == null)
                {
                    return NotFound("Data not found");
                }

                string userId = "," + UserData.UserId.ToString() + ",";
                String multiData = meeting?.CurrentlyApproval;
                if (string.IsNullOrEmpty(multiData))
                {
                    return NotFound("Invalid approval");
                }
                string[] Data = multiData.Split(userId);
                var first = Data[0].ToString();
                var second = Data[1].ToString();
                if (first == "" && second == "")
                {
                    meeting.Order++;
                    var nextOrderUser = _meetingApprovalLayerManager.GetByOrder(meeting.Order);
                    var userIds = string.Join(",", nextOrderUser.Select(c => c.UserId));
                    if (string.IsNullOrEmpty(userIds))
                    {

                        meeting.IsApproved = true;
                        meeting.Order = 0;
                        meeting.CurrentlyApproval = null;
                        meeting.AlreadyApproved += userId.Remove(0, 1);
                    }
                    else
                    {
                        userIds = "," + userIds + ",";
                        meeting.CurrentlyApproval = userIds.ToString();
                        // meeting.Order = order;
                        meeting.AlreadyApproved += userId.Remove(0, 1);
                    }
                }
                else
                {
                    if (first == "")
                    {
                        meeting.CurrentlyApproval = "," + second;
                        meeting.AlreadyApproved += userId.Remove(0, 1);
                    }
                    else if (second == "")
                    {
                        meeting.CurrentlyApproval = first + ",";
                        meeting.AlreadyApproved += userId.Remove(0, 1);
                    }
                    else
                    {
                        meeting.CurrentlyApproval = first + ("," + second);
                        meeting.AlreadyApproved += userId.Remove(0, 1);
                    }
                }
                var result = _meetingManager.Update(meeting);
                if (result)
                {
                    return Ok("Successfully Approved");
                }
                else
                {
                    return BadRequest("Approved Failed");
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Invalid Approval");
            }

        }

        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public async Task<IActionResult> MeetingListByParticipant()
        {
            try
            {
                var userId = UserData.UserId;
                string sql = $"SELECT m.Id,m.MeetingRoomId,m.Date,m.StartTime,m.EndTime,r.RoomNo,R.MeetingRoomTypeId,R.FloorNo FROM Meetings AS m INNER JOIN Participants AS p ON m.Id = p.MeetingId INNER JOIN MeetingRooms As R ON m.MeetingRoomId=r.Id WHERE m.date >= CAST(GETDATE() AS DATE) AND m.IsApproved = 1 AND p.IsUser = 1 AND p.ReferenceId = {userId} AND m.IsActive = 1";
                var data = _meetingManager.ExecuteRawSql(sql);
                var list = (from DataRow dr in data.Rows
                            select new VmMeetingListByParticipant()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                MeetingRoomId = Convert.ToInt32(dr["MeetingRoomId"]),
                                Date = Convert.ToDateTime(dr["Date"]),
                                StartTime = TimeSpan.Parse(dr["StartTime"].ToString()),
                                EndTime = TimeSpan.Parse(dr["EndTime"].ToString()),
                                RoomNo = dr["RoomNo"].ToString(),
                                MeetingRoomTypeId = Convert.ToInt32(dr["MeetingRoomTypeId"]),
                                FloorNo = dr["FloorNo"].ToString(),

                            }).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult ParticipantReject(int id, string note)
        {
            try
            {
                var getPrticipant = _participantManager.GetById(id);
                if (getPrticipant == null)
                {
                    return NotFound("Data not found");
                }
                getPrticipant.RejectNote = note;
                getPrticipant.IsRejected = true;
                var result = _participantManager.Update(getPrticipant);
                if (result)
                {
                    return Ok("Successfully Rejected");
                }
                else
                {
                    return BadRequestResult("Rejected Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }
        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult MeetingRejectByAdmin(int id, string note)
        {
            try
            {
                var application = _meetingManager.GetById(id);
                if (application == null)
                {
                    return BadRequest("Meeting Not found.");
                }
                if (application != null)
                {
                    var meeting = bookedMeetingRoomManager.GetByMeetingId(id);
                    if (meeting != null)
                    {
                        meeting.MeetingId = 0;
                        meeting.TimeSlot = "";
                    }
                    bookedMeetingRoomManager.Update(meeting);
                    application.CurrentlyApproval = "";
                    application.AlreadyApproved = "";
                    application.Order = 0;
                    application.IsActive = false;
                    application.RejectBy = UserData.UserId;
                    application.IsReject = true;
                    application.RejectDate = DateTime.Now;
                    application.RejectNote = note;
                    AuditUpdate(application);

                    if (_meetingManager.Update(application))
                    {
                        return Ok("Successfully rejected");
                    }
                    else
                    {
                        return BadRequest("Not Rejected. Please try again");
                    }
                }
                else
                {
                    return Ok("Meeting id not found");
                }

            }
            catch (Exception e)
            {

                return BadRequestResult(e?.InnerException.Message);
            }


        }
        [HttpGet]
        [MiddlewareFilter(typeof(MyCustomAuthenticationMiddlewarePipeline))]
        public IActionResult AcceptMeetingByParticipant(int id)
        {
            try
            {
                var getPrticipant = _participantManager.GetById(id);
                if (getPrticipant == null)
                {
                    return NotFound("Data not found");
                }
                getPrticipant.IsConfirmed = true;
                var result = _participantManager.Update(getPrticipant);
                if (result)
                {
                    return Ok("The Meeting Accepted Successfully");
                }
                else
                {
                    return BadRequestResult("Meeting Accepted Failed ");
                }
            }
            catch (Exception ex)
            {
                return BadRequestResult(ex.Message);
            }
        }

    }
}
