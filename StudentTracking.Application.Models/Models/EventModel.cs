namespace StudentTracking.Application.Models
{
    public class EventModel : ModelBase
    {
        public string Title { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public string EventType { get; set; }
    }
}
