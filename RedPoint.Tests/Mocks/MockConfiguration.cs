using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace RedPoint.Tests.Mocks
{
    public class MockConfiguration : IConfiguration
    {
        private readonly Dictionary<string, string> _dict;
        private readonly string _passwordFileVariant;

        public MockConfiguration(string passwordFileVariant)
        {
            _passwordFileVariant = passwordFileVariant;

            _dict = new Dictionary<string, string>();
            _dict["JwtKey"] = "TestKey";
            _dict["JwtIssuer"] = "TestIssuer";
            _dict["JwtExpireDays"] = "1";

            if (passwordFileVariant == "empty.txt")
            {
                File.CreateText("empty.txt");
            }

            if (passwordFileVariant == "full.txt")
            {
                using (var sw = File.CreateText("full.txt"))
                {
                    sw.WriteLine("test");
                    sw.WriteLine("test1");
                }
            }
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public string this[string key]
        {
            get
            {
                if (key == "BlacklistedPasswords")
                {
                    return _passwordFileVariant;
                }

                return _dict[key];
            }
            set => _dict[key] = value;
        }
    }
}