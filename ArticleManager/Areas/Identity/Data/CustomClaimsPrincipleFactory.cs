using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ArticleManager.Areas.Identity.Data
{
    public class CustomClaimsPrincipleFactory : UserClaimsPrincipalFactory<ArticleManagerUser, ArticleManagerRole>
    {
        public CustomClaimsPrincipleFactory(
            UserManager<ArticleManagerUser> userManager,
            RoleManager<ArticleManagerRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ArticleManagerUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name ?? "" ));

            var roles = await UserManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }
    }
}
