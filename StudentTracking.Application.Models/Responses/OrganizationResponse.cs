using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class OrganizationsResponse: ServiceResponse
    {
        public IEnumerable<OrganizationModel> Organizations { get; set; }
    }
    public class OrganizationResponse : ServiceResponse
    {
        public OrganizationModel Organization { get; set; }
    }
}
