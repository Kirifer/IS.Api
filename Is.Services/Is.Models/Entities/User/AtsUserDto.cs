using Is.Shared.Models;

namespace Is.Models.Entities.User
{
    public class AtsUserDto : EntityBaseDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
