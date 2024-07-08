namespace Is.Core.Authentication
{
    internal class AdminAccess : IRolePermission
    {
        public ICollection<string> GetPermissions()
        {
            return
            [
                // Resource Access
                AuthPermissions.AuthUserView,
                AuthPermissions.AuthUserManage,

                AuthPermissions.AtsUserView,
                AuthPermissions.AtsUserManage,
            ];
        }
    }
}
