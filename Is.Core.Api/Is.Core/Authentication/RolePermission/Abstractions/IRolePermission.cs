namespace Is.Core.Authentication
{
    public interface IRolePermission
    {
        ICollection<string> GetPermissions();
    }
}
