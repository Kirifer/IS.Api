using Is.Core.Database.Abstraction;
using Is.Datalayer.Interface;

namespace Is.Datalayer.Entities
{
    public class User : DbEntityIdBase
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
