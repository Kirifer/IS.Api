using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace Is.Core.Authentication
{
    public class UserContext : IUserContext
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            SetUserIdentity(httpContextAccessor);
        }

        private void SetUserIdentity(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor?.HttpContext == null) return;

            var claimsIdentity = (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity!;

            Email = GetClaimValue(claimsIdentity, ClaimTypes.Email);

            var userIdString = GetClaimValue(claimsIdentity, ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(userIdString))
            {
                UserId = new Guid(userIdString);
            }

            var fullNameString = GetClaimValue(claimsIdentity, AuthClaims.FullName);
            if (!string.IsNullOrWhiteSpace(fullNameString))
            {
                FullName = fullNameString;
            }

            var isAdmin = GetClaimValue(claimsIdentity, AuthClaims.Admin);
            if (!string.IsNullOrWhiteSpace(isAdmin))
            {
                IsAdmin = bool.TryParse(isAdmin, out var isAdminValue) ? isAdminValue : false;
            }
        }

        private string GetClaimValue(ClaimsIdentity claimsIdentity, string claimType)
        {
            var successfullyObtained = claimsIdentity.TryGetClaimValue(claimType, out var claimValue);
            return successfullyObtained ? claimValue : string.Empty;
        }
    }
}
