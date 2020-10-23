using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BlogWebApplication.Areas.Identity.IdentityHostingStartup))]
namespace BlogWebApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}