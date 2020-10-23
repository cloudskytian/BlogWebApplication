using BlogWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApplication.Pages.Articles
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
        public Article Article { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            IQueryable<IdentityUser> users = from s in _context.Users where s.UserName == User.Identity.Name select s;
            Article.AuthorId = users.FirstOrDefault().Id;
            Article.ReleaseDate = DateTime.Now;
            _context.Article.Add(Article);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Articles/Details", new { ArticleId = Article.Id });
        }
    }
}
