using StudentTracking.Application.Models.Models;
using StudentTracking.Domain;
using System.Collections.Generic;

namespace StudentTracking.Application.Models.Responses
{
    public class GalleryResponse : ServiceResponse
    {
        public int SchoolId { get; set; }
        public int ImageCount { get; set; }
        public ICollection<ImageDetailModel> Images { get; set; }
    }
}
