using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Services.Builders
{
    public class ServerBuilder
    {
        private readonly ApplicationDbContext _db;

        public ServerBuilder(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Server> BuildServer(string name, string description, bool isVisible, UserDTO userDto, Bitmap image = null)
        {
            Server server = new Server()
            {
                Name = name,
                Description = description,
                IsVisible = isVisible
            };

            //TODO
            if (!(image is null))
            {
                image.Save(AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name + "_Thumbnail");
                server.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\App_Data\\Images\\" + name +
                                   "_Thumbnail";
            }

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

            _db.Servers.Add(server);
            defChannel.Groups.Add(defGroup);
            server.Channels.Add(defChannel);
            server.Users.Add(userDto);

            await _db.SaveChangesAsync();
            return server;
        }
    }
}
