using ArticleManager.Models;
using Microsoft.AspNetCore.Identity;

namespace ArticleManager.Areas.Identity.Data
{
    public class ArticleManagerUser : IdentityUser<Guid>
    {
        [PersonalData]
        public string? Name { get; set; }

        public ICollection<UserVotes> UserVotes { get; } = new List<UserVotes>();
    }
}
