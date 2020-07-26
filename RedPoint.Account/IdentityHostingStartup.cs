using Microsoft.AspNetCore.Hosting;
using RedPoint.Account;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace RedPoint.Account
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}