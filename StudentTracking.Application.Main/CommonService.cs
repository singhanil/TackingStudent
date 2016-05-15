using StudentTracking.Application.API;
using StudentTracking.Data;

using System.Collections.Generic;
using System.Linq;

namespace StudentTracking.Application.Main
{
    public class CommonService : ICommon
    {
        StudentTrackingContext _dbContext = null;

        public CommonService(StudentTrackingContext cntx)
        {
            this._dbContext = cntx;
        }

        public IEnumerable<Class> GetAllClasses()
        {
            return this._dbContext.Classes.ToList();
        }

        public IEnumerable<Section> GetAllSections()
        {
            return this._dbContext.Sections.ToList();
        }

        public IEnumerable<TagDetail> GetAllTags()
        {
            return this._dbContext.TagDetails.ToList();
        }
    }
}
