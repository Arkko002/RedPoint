using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Account.Services;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Account
{
    public class JwtTokenGeneratorTests
    {
        public JwtTokenGeneratorTests()
        {
            var configuration = new MockConfiguration("none");
            configuration["JwtKey"] = "testKey-LongEnoughForHS256";
            configuration["JwtExpireDays"] = "1";
            configuration["JwtIssuer"] = "testIssuer";

            _tokenGenerator = new JwtTokenGenerator(configuration);
        }

        private readonly JwtTokenGenerator _tokenGenerator;

        //TODO finish checking key signing
        [Fact]
        public void GenerateToken_ShouldReturnTokenString()
        {
            var username = "testUsername";

            var identityUser = new IdentityUser();
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