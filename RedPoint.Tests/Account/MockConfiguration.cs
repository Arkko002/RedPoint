using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace RedPoint.Tests.Account
{
    public class MockConfiguration : IConfiguration
    {
        private readonly Dictionary<string, string> dict;
        private string _passwordFileVariant;
        
        public MockConfiguration(string passwordFileVariant)
        {
            _passwordFileVariant = passwordFileVariant;
            
            dict = new Dictionary<string, string>();
            dict["JwtKey"] = "TestKey";
            dict["JwtIssuer"] = "TestIssuer";
            dict["JwtExpireDays"] = "1";

            File.CreateText("empty.txt");
            using (StreamWriter sw = File.CreateText("full.txt"))
            {
                sw.WriteLine("test");
                sw.WriteLine("test1");
            }
        }
        
        public IConfigurationSection GetSection(string key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new System.NotImplementedException();
        }

        public IChangeToken GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        public string this[string key]
        {
            get
            {
                if (key == "BlacklistedPasswords")
                {
                    return _passwordFileVariant;
                }
                return dict[key];
            } 
            set => throw new System.NotImplementedException();
        }
    }
}