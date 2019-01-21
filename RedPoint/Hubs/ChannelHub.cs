using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RedPoint.Models.Users_Permissions_Models;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RedPoint.Data;
using RedPoint.Infrastructure;

namespace RedPoint.Hubs
{
    #if DEBUG
    #else
        [Authorize] 
    #endif
    public class ChannelHub : Hub<IChannelHub>
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private HubUserInputValidator _inputValidator;

        public ChannelHub(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
            _inputValidator = new HubUserInputValidator(_db);
        }

        /// <summary>
        /// Adds Channel to the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="serverId"></param>
        public async Task AddChannel(string name, string description, int serverId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            PermissionsManager permissionsManager = new PermissionsManager();

            Server server;
            if (_inputValidator.CheckIfServerExists(serverId, out server) == UserInputError.NoServer)
            {
                _logger.Warn("{0} (ID: {1}) tried to create channel in a server without permission (Server ID: {2))", user.UserName, user.Id, serverId);
                return;
            }

            if (!permissionsManager.CheckUserServerPermissions(user, server, new[] {PermissionTypes.CanManageServers})) return;

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

            await Clients.Group(server.Name).AddChannel(channelStub);
        }

        /// <summary>
        /// Deletes Channel from database
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="serverId"></param>
        public async Task RemoveChannel(string channelId, int serverId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            

            Server server;
            Channel channel;
            if (_inputValidator.CheckIfServerExists(serverId, out server) == UserInputError.NoServer)
            {
                _logger.Error("{0} (ID: {1}) tried to remove channel in a nonexistent server (Server ID: {2))", user.UserName, user.Id, serverId);
                return;
            }

            if (_inputValidator.CheckIfChannelExists(channelId, out channel) == UserInputError.NoChannel)
            {
                _logger.Error("{0} (ID: {1}) tried to remove nonexistent channel (Channel ID: {2))", user.UserName, user.Id, serverId);
                return;
            }

            PermissionsManager permissionsManager = new PermissionsManager();
            if (!permissionsManager.CheckUserServerPermissions(user, server, new[] { PermissionTypes.CanManageServers })) return;

            server.Channels.Remove(channel);
            _db.SaveChanges();

            await Clients.Group(server.Name).RemoveChannel(channel.ChannelStub);
        }
    }

    public interface IChannelHub
    {
        Task AddChannel(ChannelStub channel);
        Task RemoveChannel(ChannelStub channelStub);
    }
}