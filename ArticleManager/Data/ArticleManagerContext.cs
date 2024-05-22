using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Models;

namespace ArticleManager.Data
{
    public class ArticleManagerContext : DbContext
    {
        public ArticleManagerContext (DbContextOptions<ArticleManagerContext> options)
            : base(options)
        {
        }

        public DbSet<ArticleManager.Models.Article> Article { get; set; } = default!;
    }
}
