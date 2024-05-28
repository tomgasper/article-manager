using ArticleManager.Areas.Identity.Data;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ArticleManager.Models
{
    public class UserVotes
    {
        public int Id { get; set; }
        public int score;

        public Guid UserId { get; set; }
        public ArticleManagerUser User { get; set; } = null!;

        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
    }
}