using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services.Builders;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Services.Facades
{
    public class ChatFacade
    {
        private readonly ApplicationDbContext _db;
        private readonly HubUserInputValidator _inputValidator;
        private readonly UserManager<ApplicationUser> _userManager;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ChatFacade(ApplicationDbContext db, UserManager<ApplicationUser> userManager, HubUserInputValidator inputValidator)
        {
            _db = db;
            _userManager = userManager;
            _inputValidator = inputValidator;
        }

        public async Task<Message> CreateMessage(string userId, string text, string channelId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            UserStubManager.CheckIfUserStubExists(user, _db);

            switch (_inputValidator.CheckCreatedMessage(user, text, channelId, out var channel))
            {
                case UserInputError.InputValid:
                    MessageBuilder builder = new MessageBuilder(_db);
                    var message = await builder.BuildMessage(user.UserDto, text, channel);

                    return message;

                case UserInputError.NoChannel:
                    _logger.Error("{0} (ID: {1}) tried to write in nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                    return null;

                case UserInputError.NoPermission_CantWrite:
                    _logger.Error("{0} (ID: {1}) tried to write in channel without write permission (Channel ID: {2))", user.UserName, user.Id, channelId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ChatFacade.CreateMessage switch.");
                    return null;
            }
        }

        public async Task<(ApplicationUser user, Channel channel, bool canView, bool canWrite)?> CheckChannelChange(string userId, string channelId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            switch (_inputValidator.CheckChannelChangeAsync(user, channelId, out bool canWrite, out bool canView, out var channel))
            {
                case UserInputError.InputValid:
                    return (user: user, channel: channel, canView: canView, canWrite: canWrite);

                case UserInputError.NoChannel:
                    _logger.Error("{0} (ID: {1}) tried to join nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                    return null;
           
                default:
                    _logger.Fatal("Unknown error in ChatFacade.CheckChannelChange swtich.");
                    return null;                      
            }
        }

        public async Task<Server> CheckServerChange(string userId, int serverId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            //TODO Check if user part of server
            switch (_inputValidator.CheckServerChange(serverId, user, out var server))
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
