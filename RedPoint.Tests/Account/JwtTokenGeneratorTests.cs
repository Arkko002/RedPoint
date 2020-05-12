using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Account.Services;
using Xunit;

namespace RedPoint.Tests.Account
{
    public class JwtTokenGeneratorTests
    {
        private MockConfiguration _configuration;
        private JwtTokenGenerator _tokenGenerator;
        
        public JwtTokenGeneratorTests()
        {
            _configuration = new MockConfiguration("none");
            _configuration["JwtKey"] = "testKey-LongEnoughForHS256";
            _configuration["JwtExpireDays"] = "1";
            _configuration["JwtIssuer"] = "testIssuer";
            
            _tokenGenerator = new JwtTokenGenerator(_configuration);
        }
        
        //TODO finish checking key signing
        [Fact]
        public void GenerateToken_ShouldReturnTokenString()
        {
            string username = "testUsername";
            
            IdentityUser identityUser = new IdentityUser();
            identityUser.Id = "testId";
            
            var expires = DateTime.Now.AddDays(1);
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = _tokenGenerator.GenerateToken(username, identityUser);
            var decodedToken = tokenHandler.ReadJwtToken(tokenString);

            Assert.Contains(decodedToken.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier),
                x => x.Value == "testId");
            Assert.Contains(decodedToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub),
                x => x.Value == "testUsername");
            Assert.True(decodedToken.Issuer == "testIssuer");
        }
    }
}