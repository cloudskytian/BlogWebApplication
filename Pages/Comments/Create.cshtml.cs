using BlogWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApplication.Pages.Comments
{
    public class CreateModel : PageModel
    {
        private readonly BlogWebApplication.Data.ApplicationDbContext _context;

        public CreateModel(BlogWebApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Comment Comment { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int articleId, string fromUser, string toUser)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Comment.ArticleId = articleId;
            IQueryable<IdentityUser> users = from u in _context.Users where u.UserName == fromUser select u;
            Comment.FromUser = users.FirstOrDefault().Id;
            users = from u in _context.Users where u.UserName == toUser select u;
            Comment.ToUser = users.FirstOrDefault().Id;
            Comment.ReleaseDate = DateTime.Now;
            IQueryable<Article> articles = from a in _context.Article where a.Id == articleId select a;
            IQueryable<int> floors = from c in _context.Comment where c.ArticleId == articles.FirstOrDefault().Id select c.Floor;
            if (floors.Count() != 0)
            {
                Comment.Floor = floors.Max() + 1;
            }
            else
            {
                Comment.Floor = 1;
            }
            _context.Comment.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Articles/Details", new { articleId });
        }
    }
}
