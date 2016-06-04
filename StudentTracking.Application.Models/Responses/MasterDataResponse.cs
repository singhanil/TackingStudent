using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models
{
    public class MasterDataResponse : ServiceResponse
    {
        public IEnumerable<ClassModel> Classes { get; set; }

        public IEnumerable<SectionModel> Sections { get; set; }

        public IEnumerable<TagDetailModel> TagDetails { get; set; }

        public IEnumerable<CountryModel> Coutries { get; set; }

        public IEnumerable<StateModel> States { get; set; }
    }
}
