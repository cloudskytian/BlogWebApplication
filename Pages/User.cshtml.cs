using BlogWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApplication.Pages
{
    public class UserModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public UserModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Article> Articles { get; set; }

        public async Task<IActionResult> OnGetAsync(string userName, int? pageIndex)
        {
            IQueryable<IdentityUser> users = from u in _context.Users where u.UserName == userName select u;
            if (users.FirstOrDefault() == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = userName;
            IQueryable<Article> articlesIQ = from a in _context.Article where a.AuthorId == users.FirstOrDefault().Id select a;
            int pageSize = 3;
            Articles = await PaginatedList<Article>.CreateAsync(articlesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}
