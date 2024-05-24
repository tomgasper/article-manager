using Microsoft.AspNetCore.Identity;

namespace ArticleManager.Areas.Identity.Data
{
    public class ArticleManagerRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
