using System.Collections.Generic;

namespace RedPoint.Chat.Models.Chat
{
    public interface IGroupEntity
    {
        IEnumerable<Group> Groups { get; set; }
    }
}