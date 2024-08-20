namespace Is.Core.Config.Database
{
    public class JwtConfig
    {
        public required string CookieName { get; set; }
        public required int ExpiryDays { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Key { get; set; }
    }
}
