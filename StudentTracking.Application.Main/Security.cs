using StudentTracking.Application.API;
using StudentTracking.Data;
using StudentTracking.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class Security : ISecurity
    {
        private StudentTrackingContext _dbContext = null;

        public Security(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public UserContext Authenticate(string userId, string password)
        {
            UserContext uc = null;
            var user = this._dbContext.Users.Where(u => u.UserId.Equals(userId, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if(null != user)
            {
                if(user.Password.Equals(password))
                {
                    int  minutes = Convert.ToInt16(ConfigurationManager.AppSettings["SecurityTokenExpirataionInMinutes"]);

                    string securityToken = Guid.NewGuid().ToString("N");
                    uc = new UserContext { Id = user.UserId, Name = user.Name, Role = user.UserRole, SchoolId = user.SchoolId, SecurityToken = securityToken };
                    this._dbContext.AuthTickets.Add(new AuthTicket { SecurityToken = securityToken, ExpirationDate = DateTime.Now.AddMinutes(minutes), ModifiedDate = DateTime.Now });
                    this._dbContext.SaveChanges();
                }
            }
            return uc;
        }

        public bool ValidateToken(string securityToken)
        {
            var tokenInfo = this._dbContext.AuthTickets.Where(au => au.SecurityToken.Equals(securityToken)).FirstOrDefault();
            if(null != tokenInfo && tokenInfo.ExpirationDate > DateTime.Now)
            {
                int minutes = Convert.ToInt16(ConfigurationManager.AppSettings["SecurityTokenExpirataionInMinutes"]);

                tokenInfo.ExpirationDate = DateTime.Now.AddMinutes(minutes);
                this._dbContext.Entry(tokenInfo).State = EntityState.Modified;
                this._dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public User Get(string userId)
        {
            return this._dbContext.Users.Where(u => u.UserId.Equals(userId)).FirstOrDefault();
        }
        public IEnumerable<User> GetAll()
        {
            return this._dbContext.Users.ToList();
        }
        public IEnumerable<User> UsersList(int schoolId)
        {
            return this._dbContext.Users.Where(u => u.SchoolId == schoolId).ToList();
        }

        public User Save(User user)
        {
            if (null != user.UserId)
            {
                var entity = this._dbContext.Users.Where(e => e.UserId.Equals(user.UserId)).FirstOrDefault();

                this._dbContext.Entry(user).State = EntityState.Modified;
            }
            else
            {
                //TODO: userid needs to create
                this._dbContext.Users.Add(user);
            }
            this._dbContext.SaveChanges();

            return user;
        }
    }
}
