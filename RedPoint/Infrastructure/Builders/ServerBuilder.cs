using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using RedPoint.Data;
using RedPoint.Models.Chat_Models;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Infrastructure.Builders
{
    public class ServerBuilder
    {
        private ApplicationDbContext _db;

        public ServerBuilder(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Server> BuildServer(string name, string description, bool isVisible, Bitmap image, UserStub userStub)
        {
            Server server = new Server()
            {
                Name = name,
                Description = description,
                IsVisible = isVisible
            };

            image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail");
            server.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name +
                               "_Thumbnail";

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
            server.Users.Add(userStub);

            ServerStub serverStub = new ServerStub()
            {
                Id = server.Id,
                Name = server.Name,
                Description = description,
                ImagePath =
                    AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail",
                IsVisible = isVisible
            };
            server.ServerStub = serverStub;

            await _db.SaveChangesAsync();
            return server;
        }
    }
}
