namespace Is.Core.Authentication
{
    public interface IUserContext
    {
        Guid UserId { get; set; }

        string FullName { get; set; }

        string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
