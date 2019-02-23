using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RedPoint.Data;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Infrastructure.Facades
{
    public class ChatFacade
    {
        private ApplicationDbContext _db;
        private HubUserInputValidator _inputValidator;
        private UserManager<ApplicationUser> _userManager;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ChatFacade(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _inputValidator = new HubUserInputValidator(_db);
        }

        public async Task<Message> CreateMessage(string userId, string text, string channelId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(userId).Result;
            UserStubManager.CheckIfUserStubExists(user, _db);

            switch (_inputValidator.CheckCreatedMessage(user, text, channelId, out var channel))
            {
                case UserInputError.InputValid:
                    PermissionsManager permissionsManager = new PermissionsManager();
                    if (!permissionsManager.CheckUserChannelPermissions(user, channel, new[] { PermissionTypes.CanWrite }))
                    {
                        _logger.Error("{0} (ID: {1}) tried to write in channel without write permission (Channel ID: {2))", user.UserName, user.Id, channelId);
                        return null;
                    }

                    Message message = new Message()
                    {
                        UserStub = user.UserStub,
                        Text = text,
                        DateTimePosted = DateTime.Now
                    };

                    try
                    {
                        channel.Messages.Add(message);
                        await _db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    return message;

                case UserInputError.NoChannel:
                    _logger.Error("{0} (ID: {1}) tried to write in nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ChatFacade.CreateMessage switch.");
                    return null;
            }
        }

        public Channel CheckChannelChange(out ApplicationUser user, string userId, string channelId)
        {
            user = _userManager.FindByIdAsync(userId).Result;

            switch (_inputValidator.CheckIfChannelExists(channelId, out var channel))
            {
                case UserInputError.InputValid:               
                    return channel;

                case UserInputError.NoChannel:
                    _logger.Error("{0} (ID: {1}) tried to join nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ChatFacade.CheckChannelChange swtich.");
                    return null;                      
            }
        }

        public Server CheckServerChange(string userId, int serverId)
        {
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

            switch (_inputValidator.CheckIfServerExists(serverId, out var server))
            {
                case UserInputError.InputValid:
                    return server;

                case UserInputError.NoServer:
                    _logger.Error("{0} (ID: {1}) tried to join nonexistent server (Server ID: {2))", user.UserName, user.Id, serverId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ChatFacade.ChcekServerChange.");
                    return null;
            }
        }
    }
}
