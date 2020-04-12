using System.Collections.Generic;

namespace RedPoint.Areas.Chat.Models
{
    public interface IChatGroups
    {
        IEnumerable<Group> Groups { get; set; }
    }
}