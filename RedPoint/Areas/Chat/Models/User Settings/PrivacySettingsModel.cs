﻿namespace RedPoint.Areas.Chat.Models.User_Settings
{
    public class PrivacySettings
    {
        public int Id { get; set; }
        public bool CanBeSearched { get; set; } = true;
    }
}
