using System.IO;
using RedPoint.Account.Services.Security;
using RedPoint.Tests.Mocks;
using Xunit;

namespace RedPoint.Tests.Account.Services.Security
{
    public class AccountSecurityConfigurationProviderTests
    {
        [Fact]
        public void FileContainsPasswords_ShouldReturnListOfString()
        {
            var configuration = new MockConfiguration("full.txt");
            var provider = new AccountSecurityConfigurationProvider(configuration);

            var list = provider.GetBlacklistedPasswords();

            Assert.NotEmpty(list);
            Assert.Contains("test", list);
            Assert.Contains("test1", list);
        }

        [Fact]
        public void FileDoesntExist_ShouldThrowException()
        {
            var configuration = new MockConfiguration("nonexistent.txt");
            var provider = new AccountSecurityConfigurationProvider(configuration);

            Assert.Throws<FileNotFoundException>(() => provider.GetBlacklistedPasswords());
        }

        [Fact]
        public void FileIsEmpty_ShouldReturnEmptyList()
        {
            var configuration = new MockConfiguration("empty.txt");
            var provider = new AccountSecurityConfigurationProvider(configuration);

            var list = provider.GetBlacklistedPasswords();

            Assert.Empty(list);
        }
    }
}