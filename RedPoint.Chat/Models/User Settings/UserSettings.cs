﻿using RedPoint.Data;

namespace RedPoint.Chat.Models.User_Settings
{
    /// <summary>
    ///     Container class for specialised settings classes
    /// </summary>
    public class UserSettings : IEntity
    {
        public int Id { get; set; }

        public PrivacySettings PrivacySettings { get; set; }
        public ChatSettings ChatSettings { get; set; }
    }
}