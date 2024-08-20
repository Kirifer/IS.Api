namespace Is.Core.Authentication
{
    public static class AuthRoles
    {
        // For overall platform roles
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public static class AuthPermissions
    {
        #region Auth platform permissions
        public const string AuthUserView = "AuthUserView";                                          // Can only view users from authentication
        public const string AuthUserManage = "AuthUserManage";                                      // Can manage users
        #endregion

        #region Is platform permissions
        public const string AtsUserManage = "HrisUserManage";                                     // Can manage users on ats
        public const string AtsUserView = "HrisUserView";                                         // Can view its own details/resources
        #endregion
    }

    public static class AuthClaims
    {
        public const string FullName = "http://schemas.microsoft.com/ws/2008/06/identity/claims/fullname";
        public const string Admin = "http://schemas.microsoft.com/ws/2008/06/identity/claims/admin";
    }
}
