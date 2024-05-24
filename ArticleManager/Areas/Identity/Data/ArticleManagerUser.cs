using Microsoft.AspNetCore.Identity;

namespace ArticleManager.Areas.Identity.Data
{
    public class ArticleManagerUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }

        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
    }
}
