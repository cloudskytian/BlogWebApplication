using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BlogWebApplication.Models.User> User { get; set; }
        public DbSet<BlogWebApplication.Models.Article> Article { get; set; }
        public DbSet<BlogWebApplication.Models.Comment> Comment { get; set; }
    }
}
