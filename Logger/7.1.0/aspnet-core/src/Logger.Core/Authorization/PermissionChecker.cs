using Abp.Authorization;
using Logger.Authorization.Roles;
using Logger.Authorization.Users;

namespace Logger.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
