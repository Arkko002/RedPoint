using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedPoint.Models.Chat_Models.User_Settings
{
    public class PrivacySettings
    {
        public int Id { get; set; }
        public bool CanBeSearched { get; set; } = true;
    }
}
