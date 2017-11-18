using Abp.Authorization;
using IRACMS.Authorization.Roles;
using IRACMS.Authorization.Users;

namespace IRACMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
