using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RedPoint.Areas.Account.Models;

namespace RedPoint.Tests.Account
{
    public class MockSignInManager<T> : SignInManager<T> where T : class
    {
        public MockSignInManager()
            : base(new Mock<MockUserManager<T>>().Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<T>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<T>>>().Object,
                new AuthenticationSchemeProvider(new OptionsManager<AuthenticationOptions>(new OptionsFactory<AuthenticationOptions>(new List<IConfigureOptions<AuthenticationOptions>>(), new List<IPostConfigureOptions<AuthenticationOptions>>()))), 
                new Mock<IUserConfirmation<T>>().Object)
        { }
    }
}