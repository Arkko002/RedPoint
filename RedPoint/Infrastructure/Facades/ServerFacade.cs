using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RedPoint.Data;
using RedPoint.Infrastructure.Builders;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Infrastructure.Facades
{
    public class ServerFacade
    {
        private ApplicationDbContext _db;
        private HubUserInputValidator _inputValidator;
        private UserManager<ApplicationUser> _userManager;
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public ServerFacade(ApplicationDbContext db, UserManager<ApplicationUser> userManager, HubUserInputValidator inputValidator)
        {
            _db = db;
            _userManager = userManager;
            _inputValidator = inputValidator;
        }

        public async Task<Server> AddServer(string userId, string name, string description, bool isVisible, Bitmap image)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            switch (_inputValidator.CheckCreatedServer(user, name, description, image))
            {
                case UserInputError.InputValid:
                    ServerBuilder builder = new ServerBuilder(_db);
                    var server = await builder.BuildServer(name, description, isVisible, user.UserStub, image);
                    return server;

                default:
                    _logger.Fatal("Unknown error in ServerFacade.AddServer swtich.");
                    return null;
            }
        }

        public async Task<(Server server, bool canManageServers)?> RemoveServer(string userId, int serverId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);


            switch (_inputValidator.CheckServerRemove(user, serverId, out var server))
            {
                case UserInputError.InputValid:
                    _db.Servers.Remove(server);
                    await _db.SaveChangesAsync();
                    return (server, false);

                case UserInputError.NoPermission_CantManageServer:
                    _logger.Warn("{0} (ID: {1}) tried to delete server without permission (Server ID: {2))",
                        user.UserName, user.Id, serverId);
                    return (server, false);

                case UserInputError.NoServer:
                    _logger.Warn("{0} (ID: {1}) tried to delete nonexistent server (Server ID: {2))", user.UserName,
                        user.Id, serverId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ServerFacade.AddServer swtich.");
                    return null;
            }
        }

        public async Task<(ApplicationUser user, Server server, bool userAlreadyInServer)?> JoinServer(string userId, int serverId)
        {
            ApplicationUser user =
               await _userManager.FindByIdAsync(userId);

            switch (_inputValidator.CheckServerJoin(user, serverId, out var server))
            {
                case UserInputError.InputValid:
                    user.Servers.Add(server);
                    await _db.SaveChangesAsync();
                    return (user, server, false);

                case UserInputError.UserAlreadyInServer:
                    return (user, server, true);

                case UserInputError.NoServer:
                    _logger.Warn("{0} (ID: {1}) tried to join nonexsistent server (Server ID: {2))", user.UserName, user.Id, serverId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ServerFacade.JoinServer swtich.");
                    return null;
            }
        }

        public async Task<(Server server, bool userNotInServer)?> LeaveServer(string userId, int serverId)
        {
            ApplicationUser user =
               await _userManager.FindByIdAsync(userId);

            switch (_inputValidator.CheckServerLeave(serverId, user, out var server))
            {
                case UserInputError.InputValid:
                    server.Groups[0].Users.Remove(user.UserStub);
                    server.Users.Remove(user.UserStub);
                    await _db.SaveChangesAsync();
                    return (server, false);

                case UserInputError.UserNotInServer:
                    _logger.Warn("{0} (ID: {1}) tried to leave server that he wasn't in (Server ID: {2))", user.UserName, user.Id, serverId);
                    return (server, true);

                case UserInputError.NoServer:
                    _logger.Warn("{0} (ID: {1}) tried to join nonexsistent server (Server ID: {2))", user.UserName, user.Id, serverId);
                    return null;

                default:
                    _logger.Fatal("Unknown error in ServerFacade.LeaveServer swtich.");
                    return null;
            }
        }
    }
}
