using System.IO;
using RedPoint.Areas.Account.Services.Security;
using Xunit;

namespace RedPoint.Tests.Account
{
    public class AccountSecurityConfigurationProviderTests
    {
        [Fact]
        public void FileContainsPasswords_ShouldReturnListOfString()
        {
            MockConfiguration configuration = new MockConfiguration("full.txt");
            AccountSecurityConfigurationProvider provider = new AccountSecurityConfigurationProvider(configuration);
            
            var list = provider.GetBlacklistedPasswords();
            
            Assert.NotEmpty(list);
            Assert.Contains("test", list);
            Assert.Contains("test1", list);
        }

        [Fact]
        public void FileIsEmpty_ShouldReturnEmptyList()
        {
            MockConfiguration configuration = new MockConfiguration("empty.txt");
            AccountSecurityConfigurationProvider provider = new AccountSecurityConfigurationProvider(configuration);
            
            var list = provider.GetBlacklistedPasswords();
            
            Assert.Empty(list);
        }

        [Fact]
        public void FileDoesntExist_ShouldThrowException()
        {
            MockConfiguration configuration = new MockConfiguration("nonexistent.txt");
            AccountSecurityConfigurationProvider provider = new AccountSecurityConfigurationProvider(configuration);

            Assert.Throws<FileNotFoundException>(() => provider.GetBlacklistedPasswords());
        }
    }
}