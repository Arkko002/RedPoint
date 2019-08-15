﻿namespace RedPoint.Areas.Chat.Models
{
    public class GroupPermissions
    {
        public int Id { get; set; }

        public bool IsAdmin { get; set; } = false;
        public bool CanWrite { get; set; } = true;
        public bool CanView { get; set; } = true;
        public bool CanSendLinks { get; set; } = true;
        public bool CanAttachFiles { get; set; } = true;
        public bool CanManageServers { get; set; } = false;
        public bool CanManageChannels { get; set; } = false;
    }

    public enum PermissionTypes
    {
        IsAdmin,
        CanWrite,
        CanView,
        CanSendLinks,
        CanAttachFiles,
        CanManageServer,
        CanManageChannels
    }
}