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
    public class MockSignInManager : SignInManager<IdentityUser>
    {
        public MockSignInManager()
            : base(new Mock<MockUserManager>().Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<IdentityUser>>>().Object,
                new AuthenticationSchemeProvider(new OptionsManager<AuthenticationOptions>(new OptionsFactory<AuthenticationOptions>(new List<IConfigureOptions<AuthenticationOptions>>(), new List<IPostConfigureOptions<AuthenticationOptions>>()))), 
                new Mock<IUserConfirmation<IdentityUser>>().Object)
        { }
    }
}