using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services;
using RedPoint.Areas.Account.Services.Security;
using RedPoint.Areas.Chat.Hubs;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.Security;
using RedPoint.Data;
using RedPoint.Data.Repository;
using RedPoint.Data.UnitOfWork;
using RedPoint.Middleware;
using RedPoint.Services;
using RedPoint.Services.DtoManager;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;


                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["JwtIssuer"],
                    ValidAudience = Configuration["JwtIssuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["JwtKey"])), // TODO!!! set proper key
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSignalR();

            services.AddControllers();

            services.AddScoped(x => new EntityUnitOfWork(x.GetRequiredService<DbContext>()));
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<,>));
            
            services.AddScoped(typeof(IChatDtoFactory<Channel>), typeof(ChannelDtoFactory));
            services.AddScoped(typeof(IChatDtoFactory<ApplicationUser>), typeof(UserDtoFactory));
            services.AddScoped(typeof(IChatDtoFactory<Message>), typeof(MessageDtoFactory));
            services.AddScoped(typeof(IChatDtoFactory<Server>), typeof(ServerDtoFactory));
            
            services.AddScoped(typeof(IAccountRequestValidator), typeof(AccountRequestValidator));
            services.AddScoped(typeof(IChatRequestValidator), typeof(ChatRequestValidator));
            services.AddScoped(typeof(IDtoManager), typeof(DtoManager));

            services.AddScoped(typeof(IChatControllerService), typeof(ChatControllerService));
            services.AddScoped(typeof(IChatHubService), typeof(ChatHubService));
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //TODO
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}