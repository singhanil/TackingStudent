using System;
using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Linq;
using System.Net;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace StudentTracking.Application.Main
{
    public class NotificationService : INotificationService
    {
        private StudentTrackingContext _dbContext = null;
        public NotificationService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }
        public IEnumerable<Models.NotificationNew> Get(int schoolId, string userId)
        {
            var list = new List<Models.NotificationNew>();
            var entities = this._dbContext.Notifications.Where(N => N.SchoolId == schoolId).ToList();
            //list.Add(new NotificationModel { MessageId = "LSDFJLJ3438938DD", SchoolId = 10, From = "Adminstrator", To = "Akash", IsNew = true, Message = "IT assessment worksheet will be held on 28th July 2016, Tuesday." });
            //list.Add(new NotificationModel { MessageId = "KDSKFK39439DKFJDD", SchoolId = 10, From = "Adminstrator", To = "Akash", IsNew = true, Message = "Kindly note that the functional telephone line of the school reception is 020-XX343XX33. You are requested to call on the given number only for any correspondence." });
            //list.Add(new NotificationModel { MessageId = "KSDKFJ499944KKDK", SchoolId = 10, From = "Adminstrator", To = "Akash", IsNew = false, Message = "Kindly be informed that the school will remain closed on Wednesday, July 6, 2016 on account of Eid Ul Fitr (Ramzan Eid)" });
            if (entities.Count > 0)
                return entities.MapAsCollection<Data.Notification, Models.NotificationNew>();

            //return entities;
            return list;
        }
        public void Save(NotificationModel model)
        {

        }

        public HttpResponseMessage PostFormData()
        {
            var UploadPath = "";
            string path = "";
            Data.Notification nobj = new Data.Notification();
            foreach (string file in HttpContext.Current.Request.Files)
            {
                var FileDataContent = System.Web.HttpContext.Current.Request.Files[file];
                var myObject = System.Web.HttpContext.Current.Request;
                nobj.MessageType = myObject.Form["MessageType"];
                nobj.MessageText = myObject.Form["MessageText"];
                Console.WriteLine("My class Id: " + myObject.Form["ClassId"]);
                nobj.ClassId = Convert.ToInt32(myObject.Form["ClassId"]);
                nobj.SectionId = Convert.ToInt32(myObject.Form["SectionId"]);
                nobj.StudentId = myObject.Form["StudentId"];
                nobj.Subject = myObject.Form["Subject"];
                nobj.SchoolId = Convert.ToInt32(myObject.Form["SchoolId"]);
                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    try
                    {
                        var stream = FileDataContent.InputStream;
                        var guidKey = Guid.NewGuid();
                        var fileName = Path.GetFileName(FileDataContent.FileName);
                        fileName = guidKey + "_" + fileName;
                        //UploadPath = AppDomain.CurrentDomain.BaseDirectory + "\\Attachments\\";
                        string pathToFiles = HttpContext.Current.Server.MapPath("~/SchoolWepAPI");
                        UploadPath = pathToFiles + "\\Attachments\\";
                        Directory.CreateDirectory(UploadPath);
                        path = Path.Combine(UploadPath, fileName);

                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                            nobj.FilePath = "Attachments\\" + fileName;
                            fileStream.Dispose();
                            nobj.CreatedDate = DateTime.Now;
                            try
                            {
                                this._dbContext.Notifications.Add(nobj);
                                this._dbContext.SaveChanges();
                            }
                            catch (Exception Ex)
                            {
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);
                                return new HttpResponseMessage()
                                {
                                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                                    Content = new StringContent("File deleted." + path),
                                    ReasonPhrase = UploadPath
                                };
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        //return nobj;
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.InternalServerError,
                            Content = new StringContent("Files are not uploaded."),
                            ReasonPhrase = path
                        };
                    }
                }
            }
            //return nobj;
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Notification Added."),
                ReasonPhrase = UploadPath
            };

        }

    }
}
