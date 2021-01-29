using System;

namespace RedPoint.Chat.Exceptions.Security
{
    #nullable enable
    public class InvalidChannelRequestException : Exception
    {
        public InvalidChannelRequestException(string? message, string username, string userId, string channelName, string channelId) : base(message)
        {
            Username = username;
            UserId = userId;
            ChannelName = channelName;
            ChannelId = channelId;
        }

        public InvalidChannelRequestException(string? message, Exception? innerException, string username, string userId, string channelName, string channelId) : base(message, innerException)
        {
            Username = username;
            UserId = userId;
            ChannelName = channelName;
            ChannelId = channelId;
        }

        public string Username { get; }
        public string UserId { get; }
        public string ChannelName { get; }
        public string ChannelId { get; }
    }
}