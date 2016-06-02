using StudentTracking.Application.API;
using StudentTracking.Application.Main.Extensions;
using StudentTracking.Application.Models;
using StudentTracking.Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class OrganizationService : IOrganization
    {
        private StudentTrackingContext _context;
        public OrganizationService(StudentTrackingContext cntx)
        {
            this._context = cntx;
        }

        public IEnumerable<OrganizationModel> GetAll()
        {
            var entities = this._context.Organizations.Where(en => en.IsActive.Equals("Y", System.StringComparison.InvariantCultureIgnoreCase)).ToList();
            return entities.MapAsCollection<Organization, OrganizationModel>();
        }

        public OrganizationModel Get(int id)
        {
            var entity = this._context.Organizations.Where(en => en.ID == id).FirstOrDefault();
            if (null != entity)
                return entity.MapAs<Organization, OrganizationModel>();

            return null;
        }

        public OrganizationModel Save(OrganizationModel school)
        {
            var entity = school.MapAs<Organization>();
            entity.IsActive = "Y";
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            this._context.Organizations.Add(entity);
            this._context.SaveChanges();

            return entity.MapAs<Organization, OrganizationModel>();
        }
    }
}
