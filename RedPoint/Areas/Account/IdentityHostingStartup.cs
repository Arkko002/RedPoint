using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(RedPoint.Areas.Identity.IdentityHostingStartup))]
namespace RedPoint.Areas.Identity
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