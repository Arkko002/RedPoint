using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RedPoint.Chat.Data;
using RedPoint.Chat.Hubs;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.DtoFactories;
using RedPoint.Chat.Services.Security;
using RedPoint.Middleware;

//TODO Nlog config file
[assembly: ApiController]
namespace RedPoint.Chat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChatDbContext>(options =>
                options.UseMySql(
                        Configuration.GetConnectionString("AccountDb"), 
                        new MariaDbServerVersion(new Version(10, 5, 8)),
                        mySqlOptions => mySqlOptions
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
            );

            services.AddScoped(typeof(IChatDtoFactory<Channel, ChannelIconDto>), typeof(ChannelIconDtoFactory));
            services.AddScoped(typeof(IChatDtoFactory<Channel, ChannelDataDto>), typeof(ChannelDataDtoFactory));

            services.AddScoped(typeof(IChatDtoFactory<Server, ServerIconDto>), typeof(ServerIconDtoFactory));
            services.AddScoped(typeof(IChatDtoFactory<Server, ServerDataDto>), typeof(ServerDataDtoFactory));

            services.AddScoped(typeof(IChatDtoFactory<ChatUser, ChatUserDto>), typeof(UserDtoFactory));
            services.AddScoped(typeof(IChatDtoFactory<Message, MessageDto>), typeof(MessageDtoFactory));


            services.AddScoped(typeof(IChatRequestValidator), typeof(ChatRequestValidator));
            services.AddScoped(typeof(IChatControllerService), typeof(ChatControllerService));
            services.AddScoped(typeof(IChatHubService), typeof(ChatHubService));

            services.AddSignalR();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<JwtVerificationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub");

            });
        }
    }
}
