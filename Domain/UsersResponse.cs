using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Domain
{
    public class UsersResponse : ServiceResponse
    {
        public IEnumerable<User> Users { get; set; }
    }

    public class UserResponse : ServiceResponse
    {
        public User User{ get; set; }
    }
}
