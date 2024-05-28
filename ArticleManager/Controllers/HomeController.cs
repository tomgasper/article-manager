using ArticleManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using ArticleManager.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ArticleManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ArticleManagerUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ArticleManagerUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
                    var claims = claimsIdentity?.Claims;

                    ViewData["UserName"] = user.UserName;
                    ViewData["Email"] = user.Email;
                    ViewData["Claims"] = claims;
                }
        }       
        return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}