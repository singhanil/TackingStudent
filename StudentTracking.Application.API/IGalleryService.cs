using StudentTracking.Application.Models.Models;
using System.Collections.Generic;

namespace StudentTracking.Application.API
{
    public interface IGalleryService
    {
        ICollection<ImageDetailModel> Get(int schoolId, int pageNumber, int pageSize, out int count);
    }
}
