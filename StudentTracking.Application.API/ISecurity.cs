using StudentTracking.Application.Models;
using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface ISecurity
    {
        UserContext Authenticate(string userId, string password);
        bool ValidateToken(string securityToken);
        UserModel Get(string userId);
        IEnumerable<UserModel> GetAll();
        IEnumerable<UserModel> UsersList(int schoolId);
        UserModel Save(UserModel user);
    }
}
