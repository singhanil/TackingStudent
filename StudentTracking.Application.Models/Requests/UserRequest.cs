
namespace StudentTracking.Application.Models.Requests
{
    public class UserRequest
    {
        public string SecurityToken { get; set; }
        public UserModel User { get; set; }
    }
}
