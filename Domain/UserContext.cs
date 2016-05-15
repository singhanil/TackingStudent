
namespace StudentTracking.Domain
{
    public class UserContext
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string SecurityToken { get; set; }
        public int SchoolId { get; set; }
    }

    public class AuthResponse : ServiceResponse
    {
        public UserContext UserContext { get; set; }
    }
}
