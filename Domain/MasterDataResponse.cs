using StudentTracking.Data;
using System.Collections.Generic;

namespace StudentTracking.Domain
{
    public class MasterDataResponse : ServiceResponse
    {
        public IEnumerable<Class> Classes { get; set; }

        public IEnumerable<Section> Sections { get; set; }

        public IEnumerable<TagDetail> TagDetails { get; set; }

        public IEnumerable<Country> Coutries { get; set; }

        public IEnumerable<State> States { get; set; }
    }
}
