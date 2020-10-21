using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NLog;
using Microsoft.Extensions.Options;
using Moq;

namespace RedPoint.Tests.Mocks
{
    public class MockUserManager<T> : UserManager<T> where T : class
    {
        public MockUserManager()
            : base(new Mock<IUserStore<T>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<T>>().Object,
                new IUserValidator<T>[0],
                new IPasswordValidator<T>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<T>>>().Object)
        {
        }
    }
}