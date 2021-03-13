using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RedPoint.Chat.Data;
using RedPoint.Chat.Hubs;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.DtoFactories;
using RedPoint.Chat.Services.Security;
using RedPoint.Data.Repository;

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

        
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Debug", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            
            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ProductionPolicy", builder =>
                {
                    //TODO Prod CORS
                    builder.WithOrigins("http://localhost")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                options.HttpsPort = 443;
            });
            
            ConfigureServices(services);
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
            });
        
            //TODO Production storage of secrets, this will only work in dev env
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("ChatDb"));
            builder.Password = Configuration["DbPassword"];
            
            services.AddDbContext<ChatDbContext>(options =>
                options.UseMySql(builder.ConnectionString,
                        new MariaDbServerVersion(new Version(10, 5, 8)),
                        mySqlOptions => mySqlOptions
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
            );
            
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
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
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
            
            services.AddSignalR();
            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsClosedTypesOf(typeof(IChatDtoFactory<,>));

            builder.RegisterAssemblyTypes(dataAccess)
                .AsClosedTypesOf(typeof(IChatEntityRepositoryProxy<,>));

            builder.RegisterAssemblyTypes(dataAccess)
                .AsClosedTypesOf(typeof(IRepository<>));

            builder.RegisterType<ChatRequestValidator>().As<IChatRequestValidator>();
            builder.RegisterType<ChatControllerService>().As<IChatControllerService>(); 
            builder.RegisterType<ChatHubService>().As<IChatHubService>();
        }
        
        public void ConfigureDevelopment(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error-development");
            app.UseCors("Debug");
            
            Configure(app, env);
        }

        public void ConfigureProduction(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseCors("ProductionPolicy");
            
            Configure(app, env);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
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
