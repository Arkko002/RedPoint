using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RedPoint.Data;
using RedPoint.Models.Users_Permissions_Models;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Hubs
{
    #if DEBUG
    #else
        [Authorize] 
    #endif
    public class ServerHub : Hub<IServerHub>
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public ServerHub(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext, ApplicationDbContext db)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _db = db;
        }

        public void AddServer(string name, string description, Bitmap image)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            PermissionsManager permissionsManager = new PermissionsManager();

            if (!permissionsManager.CheckUserGroupsPermissions(user, new[] { "CanManageServers" }))
            {
                _logger.Warn("{0} (ID: {1}) tried to create server without permission", user.UserName, user.Id);
                Clients.Caller.NoAddPermission();
                return;
            }


            Server server = new Server()
            {
                Name = name,
                Description = description
            };

            image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail");
            server.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail";

            var defChannel = new Channel()
            {
                Description = "Your first channel",
                Groups = new List<Group>(),
                Messages = new List<Message>(),
                Name = "General"
            };

            var defGroup = new Group();
            {
                defGroup.Name = "Default";
            }
            defGroup.GroupPermissions = new GroupPermissions()
            {
                CanWrite = true,
                CanView = true,
                CanAttachFiles = true,
                CanSendLinks = true
            };

            defChannel.Groups.Add(defGroup);
            server.Channels.Add(defChannel);
            server.Users.Add(user.UserStub);
            Groups.AddToGroupAsync(Context.ConnectionId, server.Name);

            _db.SaveChanges();

            ServerStub serverStub = new ServerStub()
            {
                Id = server.Id,
                Name = server.Name,
                Description = description,
                ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail"
            };
            server.ServerStub = serverStub;

            Clients.Group(server.Name).AddServer(serverStub);
        }

        public void RemoveServer(int id)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            PermissionsManager permissionsManager = new PermissionsManager();

            if (!permissionsManager.CheckUserGroupsPermissions(user, new[] { "CanManageServers" }))
            {
                _logger.Warn("{0} (ID: {1}) tried to remove server without permission (Server ID:{2})", user.UserName, user.Id, id);
                Clients.Caller.NoRemovePermission();
                return;
            }
            var server = _db.Servers.Find(id);

            if (server is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to delete nonexsistent server (Server ID: {2))", user.UserName, user.Id, id);
                Clients.Caller.ServerDoesntExist();
                return;
            }

            _db.Servers.Remove(server);
            Clients.Group(server.Name).RemoveServer(server.ServerStub);
        }

        public void JoinServer(int id)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;


            var server = _db.Servers.Find(id);
            if (server is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to join nonexsistent server (Server ID: {2))", user.UserName, user.Id, id);
                Clients.Caller.ServerDoesntExist();
                return;
            }

            user.Servers.Add(server);

            server.Groups[0].Users.Add(user.UserStub);

            Groups.AddToGroupAsync(Context.ConnectionId, server.Name);
            _db.SaveChanges();

            Clients.Caller.JoinServer(id);
        }

        public void LeaveServer(int id)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;


            var server = _db.Servers.Find(id);
            if (server is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to join nonexsistent server (Server ID: {2))", user.UserName, user.Id, id);
                Clients.Caller.ServerDoesntExist();
                return;
            }

            if (!server.Users.Contains(user.UserStub))
            {
                _logger.Warn("{0} (ID: {1}) tried to leave server that he wasn't in(Server ID: {2))", user.UserName, user.Id, id);
                return;
            }

            server.Groups[0].Users.Remove(user.UserStub);

            Groups.RemoveFromGroupAsync(Context.ConnectionId, server.Name);
            server.Users.Remove(user.UserStub);
            _db.SaveChanges();

            Clients.Caller.LeaveServer(id);
        }
    }

    public interface IServerHub
    {
        void RemoveServer(ServerStub serverStub);
        void AddServer(ServerStub serverStub);
        void LeaveServer(int id);
        void JoinServer(int id);
        void NoAddPermission();
        void NoRemovePermission();
        void ServerDoesntExist();
    }
}