using System.Collections.Generic;

namespace RedPoint.Chat.Models
{
    //TODO Ambiguous interface naming / purpose. Rework to be more concise (IEntity that contains group list with attached permissions)
    public interface IChatGroups
    {
        List<Group> Groups { get; set; }
    }
}