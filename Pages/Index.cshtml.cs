using BlogWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public IndexModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<Article> Articles { get; set; }
        public Dictionary<string, string> UserIdDic { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            UserIdDic = new Dictionary<string, string>();
            IQueryable<Article> articlesIQ = from s in _context.Article select s;
            int pageSize = 3;
            Articles = await PaginatedList<Article>.CreateAsync(articlesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            List<string> userIdList = new List<string>();
            foreach (Article a in Articles)
            {
                if (!userIdList.Contains(a.AuthorId))
                {
                    userIdList.Add(a.AuthorId);
                }
            }
            foreach (string userId in userIdList)
            {
                IQueryable<IdentityUser> users = from u in _context.Users where u.Id == userId select u;
                UserIdDic.Add(userId, users.FirstOrDefault().UserName);
            }
            return Page();
        }
    }
}
