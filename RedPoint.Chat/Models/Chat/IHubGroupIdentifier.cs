using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RedPoint.Chat.Models.Chat
{
    public interface IHubGroupIdentifier
    {
        public string GroupId { get; }

        public string ComputeHash();
    }
}