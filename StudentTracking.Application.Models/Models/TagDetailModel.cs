
namespace StudentTracking.Application.Models
{
    public class TagDetailModel : ModelBase
    {
        public int ID { get; set; }
        public string TagId { get; set; }
        public string IsActive { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
