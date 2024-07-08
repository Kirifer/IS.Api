namespace Is.Core.Authentication
{
    internal class UserAccess : IRolePermission
    {
        public ICollection<string> GetPermissions()
        {
            return
            [
                // Resource Access
                AuthPermissions.AuthUserView,

                AuthPermissions.AtsUserView,
                AuthPermissions.AtsUserManage,
            ];
        }
    }
}
