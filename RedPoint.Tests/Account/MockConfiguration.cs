using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace RedPoint.Tests.Account
{
    public class MockConfiguration : IConfiguration
    {
        private string _variant;
        
        public MockConfiguration(string variant)
        {
            _variant = variant;
            
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
            get => _variant;
            set => throw new System.NotImplementedException();
        }
    }
}