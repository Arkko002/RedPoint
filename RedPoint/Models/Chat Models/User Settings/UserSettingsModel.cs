using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedPoint.Models.Chat_Models.User_Settings
{

    /// <summary>
    /// Container class for specialised settings classes
    /// </summary>
    public class UserSettings
    {
        public int Id { get; set; }

        public PrivacySettings PrivacySettings { get; set; }
        public ChatSettings ChatSettings { get; set; }
    }
}
