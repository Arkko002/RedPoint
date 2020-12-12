using System;

namespace RedPoint.Chat.Models.Chat
{
    [Flags]
    public enum PermissionTypes
    {
        None = 0,
        IsAdmin = 1,
        CanWrite = 2,
        CanView = 4,
        CanSendLinks = 8,
        CanAttachFiles = 16,
        CanManageServer = 32,
        CanManageChannels = 64
    }
}