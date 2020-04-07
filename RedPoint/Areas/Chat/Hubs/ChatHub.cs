using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Models.Dto;

namespace RedPoint.Areas.Chat.Hubs
{
    public interface IChatHub
    {
        Task ServerAdded(ServerDto server);
        Task MessageAdded(MessageDto message);
        Task ChannelAdded(ChannelDto channel);

        Task ChannelChanged(int channelId);
        Task ServerChanged(int serverId);

        Task JoinedServer(ServerDto server);
    }

    public class ChatHub : Hub<IChatHub>
    {
        public Task AddServer(ServerDto server)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, server.Name + server.Id.ToString());
            return Clients.Group(server.Name + server.Id).ServerAdded(server);
        }

        public Task AddMessage(MessageDto message)
        {

        }

        public Task AddChannel(ChannelDto channel, int serverId, string serverName)
        {
            return Clients.Group(serverName + serverId.ToString()).ChannelAdded(channel);
        }

        public Task ChangeChannel(int channelId)
        {
            //TODO Channel Group
        }

        public Task ChangeServer(int serverId)
        {
            //TODO Server Group
        }

        public Task JoinServer(ServerDto server)
        {

        }
    }
}