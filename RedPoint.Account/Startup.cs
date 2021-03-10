using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using RedPoint.Account.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedPoint.Account.Services;
using RedPoint.Account.Services.Security;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

//TODO NLog config file
[assembly: ApiController]
namespace RedPoint.Account
{
    public class Startup
    {
        private readonly string _corsAllowOrigin = "AllowAll";
        private readonly IWebHostEnvironment _env;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO Non-debug CORS
            services.AddCors(options =>
            {
                options.AddPolicy(_corsAllowOrigin, builder =>
                {
                    //builder.WithOrigins("http://localhost").;
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            
            if (!_env.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
            });

            //TODO Production storage of secrets, this will only work in dev env
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("AccountDb"));
            builder.Password = Configuration["DbPassword"];

            services.AddDbContext<AccountDbContext>(options =>
                options.UseMySql(builder.ConnectionString,
                        new MariaDbServerVersion(new Version(10, 5, 8)),
                        mySqlOptions => mySqlOptions
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<AccountDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;


                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            //TODO Use jwt middleware for token auth
            var rsa = RSA.Create();
            var pemString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Keys", Configuration.GetValue<String>("Jwt:PublicKey")));
            rsa.ImportFromPem(pemString);
            var signingKey = new RsaSecurityKey(rsa);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Asymmetric",options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = signingKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRequestValidator, AccountRequestValidator>();
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IAccountSecurityConfigurationProvider, AccountSecurityConfigurationProvider>();

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
            
            //HTTPS redirection needs to be added after CORS, otherwise CORS breaks
            app.UseCors(_corsAllowOrigin);
            app.UseRouting();
            
            
            //TODO HTTPS in prod
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
                endpoints.MapRazorPages();
            });
        }
    }
}
