namespace Is.Models.Entities.User
{
    public class AtsUserUpdateDto
    {
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}