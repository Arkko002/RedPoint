using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RedPoint.Data;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Infrastructure.Facades
{
    public class ChannelFacade
    {
        private ApplicationDbContext _db;
        private HubUserInputValidator _inputValidator;
        private UserManager<ApplicationUser> _userManager;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ChannelFacade(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _inputValidator = new HubUserInputValidator(_db);
        }

        public async Task<(Channel channel, Server server)?> CreateChannel(string userId, int serverId, string name, string description)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            switch (_inputValidator.CheckCreatedChannel(user, serverId, out var server))
            {
                case UserInputError.InputValid:
                    Channel channel = new Channel()
                    {
                        Name = name
                    };
                    ChannelStub channelStub = new ChannelStub()
                    {
                        Id = channel.Id,
                        Name = channel.Name
                    };

                    if (!(description is null))
                    {
                        channel.Description = description;
                        channelStub.Description = description;
                    }

                    server.Channels.Add(channel);
                    _db.SaveChanges();
                    channel.ChannelStub = channelStub;

                    return (channel: channel, server: server);

                case UserInputError.NoServer:
                    _logger.Warn("{0} (ID: {1}) tried to create channel in nonexistant server (Server ID: {2})", user.UserName, user.Id, serverId);
                    return null;

                case UserInputError.UserNotInServer:
                    _logger.Warn("{0} (ID: {1}) tried to create channel in sever he is not part of (Server ID: {2})", user.UserName, user.Id, serverId);
                    return null;

                case UserInputError.NoPermission_CantManageChannels:
                    _logger.Warn("{0} (ID: {1}) tried to create channel in a server without permission (Server ID: {2))", user.UserName, user.Id, serverId);
                    return null;


                default:
                    _logger.Fatal("Unknown error in ChannelFacade.CreateChannel swtich.");
                    return null;
            }
        }

        public async Task<(Server server, Channel channel)?> RemoveChannel(string userId, int serverId, string channelId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            switch (_inputValidator.CheckChannelRemove(user, serverId, channelId, out var channel, out var server))
            {
                case UserInputError.InputValid:
                    server.Channels.Remove(channel);
                    _db.SaveChanges();

                    return (server: server, channel: channel);

                case UserInputError.NoServer:
                    _logger.Warn("{0} (ID: {1}) tried to remove channel in a nonexistent server (Channel ID: {2}, Server ID: {3))",
                        user.UserName, user.Id, channelId ,serverId);
                    return null;

                case UserInputError.NoChannel:
                    _logger.Warn("{0} (ID: {1}) tried to remove nonexistent channel (Channel ID: {2), Server ID: {3})", 
                        user.UserName, user.Id, channelId, serverId);
                    return null;

                case UserInputError.NoPermission_CantManageChannels:
                    _logger.Error("{0} (ID: {1}) tried to remove channel without permission (Channel ID: {2), Server ID: {3})",
                        user.UserName, user.Id, channelId, serverId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ChannelFacade.RemoveChannel swtich.");
                    return null;
            }
         
        }
    }
}
