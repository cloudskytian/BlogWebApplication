using BlogWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApplication.Pages.Articles
{
    public class DeleteModel : PageModel
    {
        private readonly BlogWebApplication.Data.ApplicationDbContext _context;

        public DeleteModel(BlogWebApplication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article.FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Article.FindAsync(id);

            if (Article != null)
            {
                IQueryable<Comment> comments = from c in _context.Comment where c.ArticleId == Article.Id select c;
                foreach (Comment c in comments)
                {
                    _context.Comment.Remove(c);
                }
                _context.Article.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");
        }
    }
}
