
namespace StudentTracking.Domain
{
    public abstract class ServiceResponse
    {
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
