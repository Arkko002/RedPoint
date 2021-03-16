using System;

namespace RedPoint.Chat.Exceptions.Security
{
    #nullable enable
    public class UserMembershipException : Exception
    {
        public UserMembershipException(string? message, string username, string userId, string serverName, int serverId) : base(message)
        {
            Username = username;
            UserId = userId;
            ServerName = serverName;
            ServerId = serverId;
        }

        public UserMembershipException(string? message, Exception? innerException, string username, string userId, string serverName, int serverId) : base(message, innerException)
        {
            Username = username;
            UserId = userId;
            ServerName = serverName;
            ServerId = serverId;
        }

        public string Username { get; }
        public string UserId { get; }
        public string ServerName { get; }
        public int ServerId { get; }

    }
}