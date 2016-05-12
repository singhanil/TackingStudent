using StudentTracking.Application.API;
using StudentTracking.Data;
using StudentTracking.Domain;

using System;
using System.Collections.Generic;

namespace StudentTracking.Application.Main
{
    public class Security : ISecurity
    {
        private StudentTrackingEntities _dbContext = null;
        
        public Security(StudentTrackingEntities cntx)
        {
            this._dbContext = cntx;
        }

        public UserContext Authenticate(string userId, string password)
        {
            throw new NotImplementedException();
        }
        public UserContext Get(string userId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserContext> UsersList()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserContext> UsersList(string schoolId)
        {
            throw new NotImplementedException();
        }
        public UserContext Save(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
