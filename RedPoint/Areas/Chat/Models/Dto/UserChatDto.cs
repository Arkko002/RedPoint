﻿using System.Drawing;

namespace RedPoint.Areas.Chat.Models.Dto
{
    /// <summary>
    ///     DTO class for ApplicationUser
    /// </summary>
    public class UserChatDto : IDto
    {
        //Use those to retrieve ApplicationUser
        public string AppUserId { get; set; }
        public string Username { get; set; }

        public string CurrentChannelId { get; set; }
        public string CurrentServerId { get; set; }
        public Bitmap Image { get; set; }
    }
}