using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Data;
using ArticleManager.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

using ArticleManager.Areas.Identity.Data;

namespace ArticleManager.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ArticleManagerUser> _userManager;

        public ArticlesController(ApplicationDbContext context, UserManager<ArticleManagerUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
              return _context.Article != null ? 
                          View(await _context.Article.Include( a => a.UserVotes).ToListAsync()) :
                          Problem("Entity set 'ArticleManagerContext.Article'  is null.");
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article.Include(a => a.UserVotes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,ContentMarkdown")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.CreatedAt = DateTime.Now;
                article.Author = User.Identity.Name;
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        [Authorize]
        // GET: Articles/Edit/5
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

        [HttpGet]
        public async Task<IActionResult> FullPage(int? id)
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

        [Authorize]
        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article.Include( a => a.UserVotes )
                .FirstOrDefaultAsync(m => m.Id == id);

            if (article == null)
            {
                return NotFound();
            } else
            {
                if (_userManager.GetUserName(User) != article.Author)
                {
                    return LocalRedirect("/Identity/Account/AccessDenied");
                }
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Article == null)
            {
                return Problem("Entity set 'ArticleManagerContext.Article'  is null.");
            }
            var article = await _context.Article.FindAsync(id);

            if (article != null)
            {
                if (_userManager.GetUserName(User) == article.Author)
                {
                    _context.Article.Remove(article);
                }
                else
                {
                    return LocalRedirect("/Identity/Account/AccessDenied");
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Upvote(int? id)
        {
            if (id == null || _context.Article == null)
            {
                return NotFound();
            }

            var article = await _context.Article.Include( a => a.UserVotes).FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            //article.Upvotes++;

            if (ModelState.IsValid)
            {
                try
                {
                    // Don't allow multiple upvotes from the same user

                    bool alreadyUpvoted = false;
                    string userIdString = _userManager.GetUserId(User);
                    Guid currUserId;

                    if (!Guid.TryParse(userIdString, out currUserId))
                    {
                        ModelState.AddModelError("", "Invalid user ID.");
                    }

                    foreach (UserVotes pairs in article.UserVotes)
                    {
                        if (pairs.UserId == currUserId)
                        {
                            alreadyUpvoted = true;
                            break;
                        }
                    }

                    if (!alreadyUpvoted && _userManager.GetUserName(User) != article.Author)
                    {
                        UserVotes vote = new();
                        vote.Article = article;
                        vote.User = await _userManager.GetUserAsync(User);
                        // Score not needed for this simple upvote implementation
                        // Needed if the system is exteneded to allow downvotes as well
                        vote.score = 1;

                        _context.Update(vote);
                    }
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                    } catch (DBConcurrencyException)
                    {
                        if (!ArticleExists(article.Id))
                        {
                            return NotFound();
                        } else
                        {
                            throw;
                        }
                } }

            return RedirectToAction(nameof(Index));
        }
          
        private bool ArticleExists(int id)
        {
          return (_context.Article?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
