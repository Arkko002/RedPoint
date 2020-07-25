using System.Collections.Generic;

namespace RedPoint.Chat.Models
{
    public interface IChatGroups
    {
        List<Group> Groups { get; set; }
    }
}