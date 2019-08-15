namespace RedPoint.Areas.Chat.Services
{
    public enum UserInputError
    {
        InputValid,
        NoChannel,
        NoServer,
        UserNotInServer,
        UserAlreadyInServer,
        NoPermission_CantWrite,
        NoPermission_CantView,
        NoPermission_CantManageChannels,
        NoPermission_CantManageServer
    } 
}
