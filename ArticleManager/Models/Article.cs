using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ArticleManager.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Content")]
        public string? ContentMarkdown { get; set; }

        [DisplayName("Upvotes")]
        public ICollection<UserVotes> UserVotes { get; set; } = new List<UserVotes>();
    }
}