using Microsoft.AspNetCore.Hosting;
using RedPoint.Areas.Account;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace RedPoint.Areas.Account
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}