namespace Is.Shared.Models.Authentication
{
    public class AuthLoginDto
    {
        public bool Succeeded { get; set; }

        public string? Token { get; set; }
    }
}
