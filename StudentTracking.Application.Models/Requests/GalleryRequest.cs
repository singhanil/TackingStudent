namespace StudentTracking.Application.Models.Requests
{
    public class GalleryRequest : ServiceRequest
    {
        public int SchoolId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
