using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Data;
using ArticleManager.Models;
using System.Text;

namespace ArticleManager.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticleController(ApplicationDbContext context)
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

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("Id, Name, Author, Upvotes,ContentMarkdown")]Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = article.Id} );
            }
            return View(article);
        }

        // GET: Dpwmload
        public async Task<IActionResult> Download(int? id)
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

            var markdownContent = article.ContentMarkdown;
            var fileName = $"{article.Name}.md";
            var content = Encoding.UTF8.GetBytes(markdownContent);

            return File(content, "text/markdown", fileName);
        }


        private bool ArticleExists(int id)
        {
          return (_context.Article?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
