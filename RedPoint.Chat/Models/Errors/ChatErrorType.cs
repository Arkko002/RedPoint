namespace RedPoint.Chat.Models.Errors
{
    public enum ChatErrorType
    {
        NoError,
        ServerNotFound,
        ChannelNotFound,
        MessageNotFound,
        UserNotInServer,
        NoPermission
    }
}