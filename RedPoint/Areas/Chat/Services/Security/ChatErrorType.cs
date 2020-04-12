namespace RedPoint.Areas.Chat.Services.Security
{
    public enum ChatErrorType
    {
        NoError,
        ServerNotFound,
        ChannelNotFound,
        UserNotInServer,
        NoPermission
    }
}