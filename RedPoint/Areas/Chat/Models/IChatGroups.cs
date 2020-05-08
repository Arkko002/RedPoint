using System.Collections.Generic;

namespace RedPoint.Areas.Chat.Models
{
    public interface IChatGroups
    {
        List<Group> Groups { get; set; }
    }
}