using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Web;


namespace RedPoint.Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                logger.Debug(e.Message);
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true)
                .UseStartup<Startup>()
                .UseNLog();
        }
    }
}
