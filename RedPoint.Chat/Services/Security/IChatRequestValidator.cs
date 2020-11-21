using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Errors;

namespace RedPoint.Chat.Services.Security
{
    
    /// <summary>
    /// Detects potential errors and security issues in user request's within chat functionality.
    /// </summary>
    public interface IChatRequestValidator
    {
        
        /// <summary>
        /// Performs internal checks of user's request against a server (e.g. check's for appropriate permissions).
        /// </summary>
        /// <param name="server">Server that was requested by user.</param>
        /// <param name="user"></param>
        /// <param name="permissionType">Type of permission to be checked for in user's permissions.</param>
        /// <returns>ChatError object with error details. ChatErrorType is set to NoError on valid requests.</returns>
        ChatError IsServerRequestValid(Server server, ChatUser user, PermissionType permissionType);

        
        /// <summary>
        /// Performs internal checks of user's request against a channel (e.g. check's for appropriate permissions).
        /// </summary>
        /// <param name="channel">Channel that was requested by user.</param>
        /// <param name="server">Server that contains requested channel.</param>
        /// <param name="user"></param>
        /// <param name="permissionType">Type of permission to be checked for in user's permissions/</param>
        /// <returns>ChatError object with error details. ChatErrorType is set to NoError on valid requests.</returns>
        ChatError IsChannelRequestValid(Channel channel, Server server, ChatUser user,
            PermissionType permissionType);
    }
}