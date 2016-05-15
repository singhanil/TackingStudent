using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class AttendanceController : BaseApiController
    {
        StudentTrackingContext __dbContext = null;

        public AttendanceController()
        {
            this.__dbContext = new StudentTrackingContext();
        }

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


                    var attendance = this.__dbContext.sp_InsertUpdateAttendance(tagId, DateTime.Now, DateTime.Now, isIntime, ipAddress).ToList();
                    var result = attendance[0];
                    if (result.Status != "UNACTIONED")
                    {

                        if (isIntime)
                        {
                            var messageObj = this.__dbContext.Messages.FirstOrDefault(x => x.MessageCode == "CHECKIN");
                            if (messageObj != null)
                            {
                                message = messageObj.Message1;
                                var spInsertUpdateAttendanceResult = result;
                                if (spInsertUpdateAttendanceResult != null)
                                    SendMessage(spInsertUpdateAttendanceResult.ParentMobileNo, message);
                                this.__dbContext.sp_UpdateMessagingStatus((long?)spInsertUpdateAttendanceResult.AttendanceID, true);
                            }
                        }
                        else
                        {
                            var messageObj = this.__dbContext.Messages.FirstOrDefault(x => x.MessageCode == "CHECKOUT");
                            if (messageObj != null)
                            {
                                var spInsertUpdateAttendanceResult = result;
                                if (spInsertUpdateAttendanceResult != null)
                                    SendMessage(spInsertUpdateAttendanceResult.ParentMobileNo, messageObj.Message1);
                                this.__dbContext.sp_UpdateMessagingStatus((long?)spInsertUpdateAttendanceResult.AttendanceID, false);
                            }
                        }
                        scope.Complete();
                        status = "Success";
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
        private static string SendMessage(string parentMobileId, string mesage)
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

    }
}