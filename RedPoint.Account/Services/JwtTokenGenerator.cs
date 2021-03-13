using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace RedPoint.Account.Services
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string username, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Name", user.UserName)
            };

            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));
            var notBefore = DateTime.Now;
            
            using(var rsa = RSA.Create())
            {
                var pemString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Keys", _configuration.GetValue<String>("Jwt:PrivateKey")));
                rsa.ImportFromPem(pemString);
                var privateKey = new RsaSecurityKey(rsa);
                var signingCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
                
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    notBefore,
                    expires,
                    signingCredentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}
