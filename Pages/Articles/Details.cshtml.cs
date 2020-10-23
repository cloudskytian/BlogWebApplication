using BlogWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApplication.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        private readonly BlogWebApplication.Data.ApplicationDbContext _context;

        public DetailsModel(BlogWebApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Article Article { get; set; }
        public Dictionary<string, string> UserIdDic { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsAuthor { get; set; }

        public async Task<IActionResult> OnGetAsync(string articleId)
        {
            if (!int.TryParse(articleId, out int id))
            {
                return NotFound();
            }

            Article = await _context.Article.FirstOrDefaultAsync(m => m.Id == id);
            UserIdDic = new Dictionary<string, string>();
            Comments = new List<Comment>();
            IsAuthor = false;

            if (Article == null)
            {
                return NotFound();
            }

            IQueryable<IdentityUser> users = from u in _context.Users where u.Id == Article.AuthorId select u;
            List<string> userIdList = new List<string>();
            if (!userIdList.Contains(Article.AuthorId))
            {
                userIdList.Add(Article.AuthorId);
            }
            IQueryable<Comment> comments = from c in _context.Comment where c.ArticleId == Article.Id select c;
            foreach (Comment comment in comments)
            {
                Comments.Add(comment);
                if (!userIdList.Contains(comment.FromUser))
                {
                    userIdList.Add(comment.FromUser);
                }
            }
            foreach (string userId in userIdList)
            {
                users = from u in _context.Users where u.Id == userId select u;
                if (!UserIdDic.ContainsKey(userId))
                {
                    UserIdDic.Add(userId, users.FirstOrDefault().UserName);
                }
            }
            if (User.Identity.IsAuthenticated)
            {
                IQueryable<IdentityUser> userIQ = from s in _context.Users where s.UserName == User.Identity.Name select s;
                if (userIQ.FirstOrDefault().Id == Article.AuthorId)
                {
                    IsAuthor = true;
                }
            }

            return Page();
        }
    }
}
