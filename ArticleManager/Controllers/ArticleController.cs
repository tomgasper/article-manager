using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Data;
using ArticleManager.Models;

namespace ArticleManager.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleManagerContext _context;

        public ArticleController(ArticleManagerContext context)
        {
            _context = context;
        }

        // GET: Article
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        private bool ArticleExists(int id)
        {
          return (_context.Article?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
