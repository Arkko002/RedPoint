//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.SignalR;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using RedPoint.Models.Users_Permissions_Models;
//using RedPoint.Models;
//using RedPoint.Models.Chat_Models;

//namespace RedPoint.Hubs
//{
//    [Authorize]
//    public class ServerHub : Hub<IServerHub>
//    {
//        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
//        private readonly ApplicationDbContext _db = new ApplicationDbContext();

//        public void AddServer(string name, string description, Bitmap image)
//        {
//            UserManager<ApplicationUser>
//                manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
//            ApplicationUser user = manager.FindById(Context.User.Identity.GetUserId());
//            PermissionsManager permissionsManager = new PermissionsManager();

//            if (!permissionsManager.CheckUserGroupsPermissions(user, new[] {"CanManageServers"}))
//            {
//                _logger.Warn("{0} (ID: {1}) tried to create server without permission", user.UserName, user.Id);
//                Clients.Caller.NoAddPermission();
//                return;
//            }


//            Server server = new Server()
//            {
//                Name = name,
//                Description = description
//            };

//            image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail");
//            server.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail";

//            var defChannel = new Channel()
//            {
//                Description = "Your first channel",
//                Groups = new List<Group>(),
//                Messages = new List<Message>(),
//                Name = "General"
//            };

//            var defGroup = new Group();
//            {
//                defGroup.Name = "Default";
//            }
//            defGroup.GroupPermissions = new GroupPermissions()
//            {
//                CanWrite = true,
//                CanView = true,
//                CanAttachFiles = true,
//                CanSendLinks = true
//            };

//            defChannel.Groups.Add(defGroup);
//            server.Channels.Add(defChannel);
//            server.Users.Add(user.UserStub);
//            Groups.Add(Context.ConnectionId, server.Name);

//            _db.SaveChanges();

//            ServerStub serverStub = new ServerStub()
//            {
//                Id = server.Id,
//                Name = server.Name,
//                Description = description,
//                ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail"
//            };
//            server.ServerStub = serverStub;

//            Clients.Group(server.Name).AddServer(serverStub);
//        }

//        public void RemoveServer(int id)
//        {
//            UserManager<ApplicationUser>
//                manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
//            ApplicationUser user = manager.FindById(Context.User.Identity.GetUserId());
//            PermissionsManager permissionsManager = new PermissionsManager();

//            if (!permissionsManager.CheckUserGroupsPermissions(user, new[] {"CanManageServers"}))
//            {
//                _logger.Warn("{0} (ID: {1}) tried to remove server without permission (Server ID:{2})", user.UserName, user.Id, id);
//                Clients.Caller.NoRemovePermission();
//                return;
//            }
//            var server = _db.Servers.Find(id);

//            if (server is null)
//            {
//                _logger.Warn("{0} (ID: {1}) tried to delete nonexsistent server (Server ID: {2))", user.UserName, user.Id, id);
//                Clients.Caller.ServerDoesntExist();
//                return;
//            }

//            _db.Servers.Remove(server);
//            Clients.Group(server.Name).RemoveServer(server.ServerStub);
//        }

//        public void JoinServer(int id)
//        {
//            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
//            ApplicationUser user = manager.FindById(Context.User.Identity.GetUserId());

//            var server = _db.Servers.Find(id);
//            if (server is null)
//            {
//                _logger.Warn("{0} (ID: {1}) tried to join nonexsistent server (Server ID: {2))", user.UserName, user.Id, id);
//                Clients.Caller.ServerDoesntExist();
//                return;
//            }

//            user.Servers.Add(server);

//            server.Groups[0].Users.Add(user.UserStub);

//            Groups.Add(Context.ConnectionId, server.Name);
//            _db.SaveChanges();

//            Clients.Caller.JoinServer(id);
//        }

//        public void LeaveServer(int id)
//        {
//            UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
//            ApplicationUser user = manager.FindById(Context.User.Identity.GetUserId());

//            var server = _db.Servers.Find(id);
//            if (server is null)
//            {
//                _logger.Warn("{0} (ID: {1}) tried to join nonexsistent server (Server ID: {2))", user.UserName, user.Id, id);
//                Clients.Caller.ServerDoesntExist();
//                return;
//            }

//            if (!server.Users.Contains(user.UserStub))
//            {
//                _logger.Warn("{0} (ID: {1}) tried to leave server that he wasn't in(Server ID: {2))", user.UserName, user.Id, id);
//                return;
//            }

//            server.Groups[0].Users.Remove(user.UserStub);

//            Groups.Remove(Context.ConnectionId, server.Name);
//            server.Users.Remove(user.UserStub);
//            _db.SaveChanges();

//            Clients.Caller.LeaveServer(id);
//        }
//    }

//    public interface IServerHub
//    {
//        void RemoveServer(ServerStub serverStub);
//        void AddServer(ServerStub serverStub);
//        void LeaveServer(int id);
//        void JoinServer(int id);
//        void NoAddPermission();
//        void NoRemovePermission();
//        void ServerDoesntExist();
//    }
//}