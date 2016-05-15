using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ISecurity
    {
        UserContext Authenticate(string userId, string password);
        bool ValidateToken(string securityToken);
        UserContext Get(string userId);
        IEnumerable<UserContext> UsersList();
        IEnumerable<UserContext> UsersList(string schoolId);
        UserContext Save(string userId);
    }
}
