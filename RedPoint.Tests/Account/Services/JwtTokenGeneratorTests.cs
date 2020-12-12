using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RedPoint.Account.Services;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Account.Services
{
    public class JwtTokenGeneratorTests
    {
        public JwtTokenGeneratorTests()
        {
            var configuration = new MockConfiguration("none")
            {
                ["Jwt:Key"] = "testKey-LongEnoughForHS256",
                ["Jwt:ExpireDays"] = "1",
                ["Jwt:Issuer"] = "testIssuer"
            };

            _tokenGenerator = new JwtTokenGenerator(configuration);
        }

        private readonly JwtTokenGenerator _tokenGenerator;

        //TODO finish checking key signing
        [Fact]
        public void GenerateToken_ShouldReturnTokenString()
        {
            const string username = "testUsername";

            var identityUser = new IdentityUser { Id = "testId" };

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