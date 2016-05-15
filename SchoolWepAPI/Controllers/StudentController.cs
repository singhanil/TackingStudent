using StudentTracking.Application.API;
using StudentTracking.Application.Main;
using StudentTracking.Data;
using StudentTracking.Domain;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolWepAPI.Controllers
{
    public class StudentController : BaseApiController
    {
        StudentTrackingContext _dbContext = null;

        public StudentController()
        {
            this._dbContext = new StudentTrackingContext();
        }

        [Route("api/Student/{securityToken}/{schoolId}")]
        public HttpResponseMessage Get(string securityToken, int schoolId)
        {

            AllStudentResponse response = null;
            if (IsValid(securityToken))
            {
                IStudentDetails student = new StudentDetails(this._dbContext);
                response = new AllStudentResponse { Status = "OK" };
                response.Students = student.GetAll(schoolId);
                CurrentLoggerProvider.Info(string.Format("Retrieved Student Details. Count = {0}", response.Students.Count()));
            }
            else
            {
                response = new AllStudentResponse { Status = "Error", ErrorCode = "ERR1001", ErrorMessage = "Invalid or expired token" };
                CurrentLoggerProvider.Info("Invalid Request. Student Id: {0}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        private bool IsValid(string securityToken)
        {
            ISecurity auth = new Security(this._dbContext);
            return auth.ValidateToken(securityToken);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
