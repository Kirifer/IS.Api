namespace Is.Models.Entities.User
{
    public class AtsUserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

    }
}