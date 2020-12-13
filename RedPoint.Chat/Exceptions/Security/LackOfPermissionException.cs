using System;
using RedPoint.Chat.Models.Chat;

namespace RedPoint.Chat.Exceptions.Security
{
    #nullable enable
    public class LackOfPermissionException : Exception
    {
        public LackOfPermissionException(string? message, string username, string userId, string entityName, int entityId, PermissionTypes permission) : base(message)
        {
            Username = username;
            UserId = userId;
            EntityName = entityName;
            EntityId = entityId;
            Permission = permission;
        }

        public LackOfPermissionException(string? message, Exception? innerException, string username, string userId, string entityName, int entityId, PermissionTypes permission) : base(message, innerException)
        {
            Username = username;
            UserId = userId;
            EntityName = entityName;
            EntityId = entityId;
            Permission = permission;
        }

        public string Username { get; }
        public string UserId { get; }
        public string EntityName { get; }
        public int EntityId { get; }
        public PermissionTypes Permission { get; }
    }
}