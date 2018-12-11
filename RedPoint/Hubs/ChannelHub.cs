using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RedPoint.Models.Users_Permissions_Models;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RedPoint.Data;

namespace RedPoint.Hubs
{
    #if DEBUG
    #else
        [Authorize] 
    #endif
    public class ChannelHub : Hub<IChannelHub>
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public ChannelHub(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext, ApplicationDbContext db)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _db = db;
        }

        /// <summary>
        /// Adds Channel to the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="serverId"></param>
        public void AddChannel(string name, string description, int serverId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            PermissionsManager permissionsManager = new PermissionsManager();

            Server server = _db.Servers.Find(serverId);
            if (server is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to create channel in a server without permission (Server ID: {2))", user.UserName, user.Id, serverId);
                return;
            }

            if (!permissionsManager.CheckUserGroupsPermissions(user, server, new[] { "CanManageChannels" })) return;

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

            Clients.Group(server.Name).AddChannel(channelStub);
        }

        /// <summary>
        /// Deletes Channel from database
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="serverId"></param>
        public void RemoveChannel(int channelId, int serverId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            PermissionsManager permissionsManager = new PermissionsManager();

            Server server = _db.Servers.Find(serverId);
            if (server is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to remove channel in a nonexistent server (Server ID: {2))", user.UserName, user.Id, serverId);
                return;
            }

            if (!permissionsManager.CheckUserGroupsPermissions(user, server, new[] { "CanManageChannels" })) return;

            Channel channel = _db.Channels.Find(channelId);
            if (channel is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to remove nonexistent channel (Channel ID: {2))", user.UserName, user.Id, serverId);
                return;
            }

            server.Channels.Remove(channel);
            _db.SaveChanges();

            Clients.Group(server.Name).RemoveChannel(channel.ChannelStub);
        }
    }

    public interface IChannelHub
    {
        void AddChannel(ChannelStub channel);
        void RemoveChannel(ChannelStub channelStub);
    }
}