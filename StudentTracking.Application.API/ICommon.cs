using StudentTracking.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.Application.API
{
    public interface ICommon
    {
        IEnumerable<Class> GetAllClasses();
        IEnumerable<Section> GetAllSections();
        IEnumerable<TagDetail> GetAllTags();
    }
}
