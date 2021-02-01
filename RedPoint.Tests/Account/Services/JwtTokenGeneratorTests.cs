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
        private readonly JwtTokenGenerator _tokenGenerator;

        public JwtTokenGeneratorTests()
        {
            var configuration = new MockConfiguration("none")
            {
                ["Jwt:PrivateKey"] = "MIIEogIBAAKCAQEApTU5T4kF54Eqid0EYfALxYJdz6LAcJxNyXrVa8BFTb9qZyPyBUBNWI6bk/5yjkXgMStb+IwEcIXpMPdoVAY0W7qGsRq7W5W+abOFES4qkgcilhmZONBL0Zeau66qcUPIgt6AJNZqNmmZZ1Swv60u/kuPwlFx2eRCVAJDpTJZZwicVnSELYTHPzbUs24LQaj3ABajZBKtAVRo1giU6s+1Lnl4/v9+yVT5RBzvPcsmjxJeU5JmEt2gDZqEsDmpoeYasEkRsTq8jZNB6a5lVEvwlljktksmUtImcjLvibiRAGjMW86NiiYLjESTEBrKIteKhEH0DoioAoFzUBjapGWKrQIDAQABAoIBAHOZtIqE1M7TQRLKxqBJOdNwj3gU6BdkJ8IN43zMQzJN+IRHULh/8B9BWnfKGhqXpnKBZo+aWhjqeuu6kxQIa2asNEeE7wj81QpdhYUqCru3pmnla6OnjQYR0UH2zGkJBysbygr8xcFhTuhRCIR3CGUENonAN6xB3m+uTeswOgKvj8/873cn7UxeQQTzozXjsYp3f7UfiA0uJySVHV3yUCJFK11Il+NxLvjpdw29ncnxuTUVYR/zZM8uHOOeo5qQ5GvI25iMn+emxOSXxm1m14JISWdc5ejSjBOM2lrxhJ5XCrDNzBJrx/BFKrw78+3yhl3/ns9M9+eY7l2gSg6TxHkCgYEA6GT5zK4SvArj4DKeOAGqmwKHGFhod9pgMMix+8mGmPGy2brXwoCsYKTTISxNH/1pvDGL4i5lh8EGaBxq8ws28mzYfdGnTLzIt9CweaKkIAFkYDTKUO8cTAIDvGDaHJbo6oZvKG87+G4VEtUVziKxivcdN5m0dbooFAGc9VpcD0sCgYEAtf0tDon0fe7qE0/CwAyKaj9Fgfnn4EAS6NJltzmD5J8tD+gIqw5iCvj8EzpobJimkpNhIN0QW4rIw9FicLv44oBZKxU41503e8bQgeCszOb++D5BYFaWzHWPh/goPvpJsXIXdGCj1ZOiVmNJlKLw+XDzwywoZTa3DqMWgkveeucCgYBD2H7pF4TWqyM95+kzFqF1IbbJD75oBzP/6ge0J3CJJD7/u8GTwjcQ++27iJB3n2cIDzr0bHEtwdeZ+3npvMNs4QQPL271Q0QiDeYHjMj3oxBn2eGa4UPUmN34WBo1MIWNOnQnTNooPBMg7V6xUIWeuWgpPFFn8VIUDSiCpL+towKBgA305pmw1sE5q1XjzgmwbIUNaSU4pyG5iDm2uPo+PPExi+EmaFPF/Jre0WYgGpYJduzSxKYijfiXQiJSUnxWzhWAxlXZgMx4UCL78k2jj1z/chpTm+vpeBMiCOnijCOYSkKDf2z7ZFUix1Zcsu3lORnIIcmb/1UTxyImO7muW9eNAoGAd/6epupITJbWItujddk5WtQf1FbCKttEpJzZQV+S9h3ed55QK5wK7kXyTR2LFlPtb+R5ul0oeZwpu0ANNmUcZohTZPCd27mBhFAeGzPjWtA2dcQ0yS+cKPZOZWP/WbQygaX8LsB4lvA0PTTyXeBFb4cyAQNctD1CxpIXOmb7GPo=",
                ["Jwt:PublicKey"] = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApTU5T4kF54Eqid0EYfALxYJdz6LAcJxNyXrVa8BFTb9qZyPyBUBNWI6bk/5yjkXgMStb+IwEcIXpMPdoVAY0W7qGsRq7W5W+abOFES4qkgcilhmZONBL0Zeau66qcUPIgt6AJNZqNmmZZ1Swv60u/kuPwlFx2eRCVAJDpTJZZwicVnSELYTHPzbUs24LQaj3ABajZBKtAVRo1giU6s+1Lnl4/v9+yVT5RBzvPcsmjxJeU5JmEt2gDZqEsDmpoeYasEkRsTq8jZNB6a5lVEvwlljktksmUtImcjLvibiRAGjMW86NiiYLjESTEBrKIteKhEH0DoioAoFzUBjapGWKrQIDAQAB",
                ["Jwt:ExpireDays"] = "1",
                ["Jwt:Issuer"] = "testIssuer"
            };

            _tokenGenerator = new JwtTokenGenerator(configuration);
        }

        [Fact]
        public void GenerateToken_ShouldReturnTokenString()
        {
            const string username = "testUsername";

            var identityUser = new IdentityUser {Id = "testId"};

            var expires = DateTime.Now.AddDays(1);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = _tokenGenerator.GenerateToken(username, identityUser);
            var decodedToken = tokenHandler.ReadJwtToken(tokenString);

            Assert.Contains(decodedToken.Claims.Where(x => x.Type == "id"),
                x => x.Value == "testId");
            Assert.Contains(decodedToken.Claims.Where(x => x.Type == JwtRegisteredClaimNames.Sub),
                x => x.Value == "testUsername");
            Assert.True(decodedToken.Issuer == "testIssuer");
        }
    }
}