using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentTracking.Data;
using System.Transactions;
using Antlr.Runtime;

namespace StudentTracking.Web.Controllers
{
    public class AttendanceController : ApiController
    {
        private readonly StudentTrackingEntities _db = new StudentTrackingEntities();
        // GET: api/Attendance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Attendance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Attendance
        [HttpPost]
        public String PostTagInformation(string tagId, string ipAddress, bool isIntime)
        {
            try
            {
                var message = "";
                var status = "unchanged";
                //This stored proc must provide out parameter to say if it was an insert or update
                //If it is an insert for this we need to check the isIntime then we need to send message to parent that the Child has come to school
                //If it is an update then we need to send message that the child has left the school


                using (var scope = new TransactionScope())
                {


                    var attendance = _db.sp_InsertUpdateAttendance(tagId, DateTime.Now, DateTime.Now, isIntime, ipAddress).ToList();
                    var result = attendance[0];
                    if (result.Status != "UNACTIONED")
                    {
                        
                        if (isIntime)
                        {
                            var messageObj = _db.Messages.FirstOrDefault(x => x.MessageCode == "CHECKIN");
                            if (messageObj != null)
                            {
                                message = messageObj.Message1;
                                var spInsertUpdateAttendanceResult = result;
                                if (spInsertUpdateAttendanceResult != null)
                                    SendMessage(spInsertUpdateAttendanceResult.ParentMobileNo, message);
                                _db.sp_UpdateMessagingStatus((long?) spInsertUpdateAttendanceResult.AttendanceID, true);
                            }
                        }
                        else
                        {
                            var messageObj = _db.Messages.FirstOrDefault(x => x.MessageCode == "CHECKOUT");
                            if (messageObj != null)
                            {
                                var spInsertUpdateAttendanceResult = result;
                                if (spInsertUpdateAttendanceResult != null)
                                    SendMessage(spInsertUpdateAttendanceResult.ParentMobileNo, messageObj.Message1);
                                _db.sp_UpdateMessagingStatus((long?) spInsertUpdateAttendanceResult.AttendanceID, false);
                            }
                        }
                        scope.Complete();
                        status= "Success";
                    }
                    return status;
                }
            }
            catch (TransactionAbortedException ex)
            {
                return "Failed";
            }
            catch (ApplicationException ex)
            {
                return "Failed";
            }


        }

        

        // PUT: api/Attendance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Attendance/5
        public void Delete(int id)
        {
            var oo = "eee";
        }

        private static string SendMessage(string parentMobileId,string mesage)
        {
            try
            {
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
        [HttpPost]
        public void Post([FromBody] Attendanceinfo p)
        {
            var uu = p.IpAddress;
        } 
    }
    public class Attendanceinfo
    {
        public string TagId { get; set; }
        public string IpAddress { get; set; }
        public bool IsIntime { get; set; }
    } 
}
