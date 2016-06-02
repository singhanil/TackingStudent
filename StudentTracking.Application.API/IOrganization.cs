using StudentTracking.Application.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IOrganization
    {
        IEnumerable<OrganizationModel> GetAll();
        OrganizationModel Get(int id);
        OrganizationModel Save(OrganizationModel school);
    }
}
