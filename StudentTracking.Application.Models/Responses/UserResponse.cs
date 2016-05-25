using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class UserResponse : ServiceResponse
    {
        public UserModel User { get; set; }
    }

    public class UsersResponse : ServiceResponse
    {
        public IEnumerable<UserModel> Users { get; set; }
    }
}
