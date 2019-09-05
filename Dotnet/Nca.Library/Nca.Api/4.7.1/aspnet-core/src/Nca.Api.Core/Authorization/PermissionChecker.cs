using Abp.Authorization;
using Nca.Api.Authorization.Roles;
using Nca.Api.Authorization.Users;

namespace Nca.Api.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
