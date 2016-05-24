using StudentTracking.Data;
using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ISecurity
    {
        UserContext Authenticate(string userId, string password);
        bool ValidateToken(string securityToken);
        User Get(string userId);
        IEnumerable<User> GetAll();
        IEnumerable<User> UsersList(int schoolId);
        User Save(User user);
    }
}
