using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RedPoint.Areas.Account.Models;

namespace RedPoint.Tests.Account
{
    public class MockUserManager : UserManager<IdentityUser>
    {
        public MockUserManager()
            : base(new Mock<IUserStore<IdentityUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<IdentityUser>>().Object,
                new IUserValidator<IdentityUser>[0],
                new IPasswordValidator<IdentityUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<IdentityUser>>>().Object)
        { }

    }
}