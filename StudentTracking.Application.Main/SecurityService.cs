using StudentTracking.Application.API;
using StudentTracking.Application.Models;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Data;
using StudentTracking.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class SecurityService : ISecurity
    {
        private StudentTrackingContext _dbContext = null;

        public SecurityService(StudentTrackingContext cntx)
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

        public UserModel Get(string userId)
        {
            var entity = this._dbContext.Users.Where(u => u.UserId.Equals(userId)).FirstOrDefault();

            if (null != entity)
                return entity.MapAs<User, UserModel>();

            return null;
        }
        public IEnumerable<UserModel> GetAll()
        {
            var entities = this._dbContext.Users.ToList();

            if (null != entities)
                return entities.MapAsCollection<User, UserModel>();

            return null;
        }

        public IEnumerable<UserModel> UsersList(int schoolId)
        {
            var entities = this._dbContext.Users.Where(u => u.SchoolId == schoolId).ToList();

            if (null != entities)
                return entities.MapAsCollection<User, UserModel>();

            return null;
        }

        public UserModel Save(UserModel model)
        {
            var entity = this._dbContext.Users.Where(e => e.UserId.Equals(model.UserId, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            
            entity = _populateValues(entity, model);

            if (!string.IsNullOrWhiteSpace(model.UserId))
            {
                if (null != entity)
                    this._dbContext.Entry(entity).State = EntityState.Modified;
                else
                    this._dbContext.Users.Add(entity);
            }
            else
                this._dbContext.Users.Add(entity);

            this._dbContext.SaveChanges();

            return entity.MapAs<User, UserModel>();
        }

        private User _populateValues(User entity, UserModel model)
        {
            if (null == entity)
            {
                entity = new User();
                entity.CreatedDate = DateTime.Now;
                entity.SchoolId = model.SchoolId.Value;
                entity.UserId = model.UserId;
                entity.EmailId = model.EmailId;
            }
            entity.CreatedDate = entity.CreatedDate;
            entity.ModifiedDate = DateTime.Now;
            entity.ContactNumber = model.ContactNumber;
            entity.EmailId = model.EmailId;
            entity.Name = model.Name;
            entity.Password = model.Password;
            entity.UserRole = model.UserRole;
            entity.ContactNumber = model.ContactNumber;

            return entity;
        }
    }
}
