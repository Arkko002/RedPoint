using System;

namespace RedPoint.Chat.Exceptions.Security
{
    #nullable enable
    public class InvalidServerRequestException : Exception
    {
        public InvalidServerRequestException(string? message, string username, string userId, string serverName, string serverId) : base(message)
        {
            Username = username;
            UserId = userId;
            ServerName = serverName;
            ServerId = serverId;
        }

        public InvalidServerRequestException(string? message, Exception? innerException, string username, string userId, string serverName, string serverId) : base(message, innerException)
        {
            Username = username;
            UserId = userId;
            ServerName = serverName;
            ServerId = serverId;
        }

        public string Username { get; }
        public string UserId { get; }
        public string ServerName { get; }
        public string ServerId { get; }
        
        
    }
}