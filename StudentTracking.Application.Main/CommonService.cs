using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using System;
using System.Web;

namespace StudentTracking.Application.Main
{
    public class CommonService : ICommon
    {
        StudentTrackingContext _dbContext = null;

        public CommonService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<ClassModel> GetAllClasses()
        {
            var entities = this._dbContext.Classes.ToList();
            if (null != entities)
                return entities.MapAsCollection<Class, ClassModel>();

            return null;
        }

        public IEnumerable<SubjectModel> GetAllSubjects()
        {
            var entities = this._dbContext.Subjects.ToList();
            if (null != entities)
                return entities.MapAsCollection<Subject, SubjectModel>();

            return null;
        }

        public IEnumerable<SectionModel> GetAllSections()
        {
            var entities = this._dbContext.Sections.ToList();
            if (null != entities)
                return entities.MapAsCollection<Section, SectionModel>();

            return null;
        }

        public IEnumerable<TagDetailModel> GetAllTags()
        {
            var entities = this._dbContext.TagDetails.Where(t=>t.IsAvailable.Equals("Y")).ToList();
            if (null != entities && entities.Count > 0)
                return entities.MapAsCollection<TagDetail, TagDetailModel>();

            return new List<TagDetailModel>();
        }

        public IEnumerable<CountryModel> GetAllCountries()
        {
            var entities = this._dbContext.Countries.ToList();
            if (null != entities)
                return entities.MapAsCollection<Country, CountryModel>();

            return null;
        }

        public IEnumerable<StateModel> GetAllStates()
        {
            var entities = this._dbContext.States.ToList();
            if (null != entities)
                return entities.MapAsCollection<State, StateModel>();

            return null;
        }

        public IEnumerable<StateModel> FindStates(string countyCode)
        {
            var entities = this._dbContext.States.Where(s => s.CountryCode.Equals(countyCode)).ToList();
            if (null != entities)
                return entities.MapAsCollection<State, StateModel>();

            return null;
        }

        public HttpResponseMessage UploadDocument()
        {
            var UploadPath = "";
            string path = "";
            Data.Document docObj = new Data.Document();
            foreach (string file in HttpContext.Current.Request.Files)
            {
                var FileDataContent = System.Web.HttpContext.Current.Request.Files[file];
                var myObject = System.Web.HttpContext.Current.Request;
                docObj.UserId = myObject.Form["UserId"];
                docObj.SchoolId = Convert.ToInt32(myObject.Form["SchoolId"]);
                if(!string.IsNullOrEmpty(myObject.Form["ClassId"]))
                    docObj.ClassId =  Convert.ToInt32(myObject.Form["ClassId"]);
                docObj.CreatedDate = DateTime.Now;
                docObj.UpdatedDate = DateTime.Now;
                var guidKey = Guid.NewGuid();
                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    try
                    {
                        var stream = FileDataContent.InputStream;
                        var fileName = guidKey +"_"+ FileDataContent.FileName;
                        docObj.DocumentName = fileName;
                        //string driveLetter = Path.GetPathRoot(Environment.CurrentDirectory);
                        UploadPath = AppDomain.CurrentDomain.BaseDirectory + "FileStore\\" + docObj.SchoolId + "\\";
                        Directory.CreateDirectory(UploadPath);
                        path = Path.Combine(UploadPath, fileName);

                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                            fileStream.Dispose();
                            
                            try
                            {
                                this._dbContext.Documents.Add(docObj);
                                this._dbContext.SaveChanges();
                            }
                            catch (Exception Ex)
                            {
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);
                                return new HttpResponseMessage()
                                {
                                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                                    Content = new StringContent("Document upload failed."),
                                    ReasonPhrase = "Document upload failed."
                                };
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        return new HttpResponseMessage()
                        {
                            StatusCode = System.Net.HttpStatusCode.InternalServerError,
                            Content = new StringContent("Document upload failed."),
                            ReasonPhrase = "Document upload failed."
                        };
                    }
                }
            }
            
            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("Document uploaded successfully."),
                ReasonPhrase = string.Empty
            };

        }


    }
}
