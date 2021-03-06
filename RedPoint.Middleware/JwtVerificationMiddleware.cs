using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace RedPoint.Middleware
{
    public class JwtVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtVerificationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(' ').Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var rsa = RSA.Create();
            var pemString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Keys", _configuration["Jwt:PublicKey"]));
            rsa.ImportFromPem(pemString);
            var signingKey = new RsaSecurityKey(rsa);
            
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            context.Items["UserToken"] = jwtToken;
        }
    }
}