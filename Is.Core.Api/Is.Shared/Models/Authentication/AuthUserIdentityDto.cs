namespace Is.Shared.Models.Authentication
{
    public class AuthUserIdentityDto
    {
        public Guid UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
