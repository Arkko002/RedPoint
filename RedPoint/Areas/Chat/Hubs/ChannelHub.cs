using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.Facades;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Hubs
{
    /// <summary>
    /// Hub for managing the Channels.
    /// </summary>
    public class ChannelHub : Hub<IChannelHub>
    {
        private readonly ChannelFacade _channel;

        public ChannelHub(UserManager<ApplicationUser> userManager, ApplicationDbContext db, HubUserInputValidator inputValidator)
        {
            _channel = new ChannelFacade(db, userManager, inputValidator);
        }

        /// <summary>
        /// Delegates the ChannelCreation. Sends the created Channel stub to people in Server on success.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="serverId"></param>
        public async Task AddChannel(string name, string description, int serverId)
        {
           //TODO Add images
           var resultTuple =  await _channel.CreateChannel(Context.UserIdentifier, serverId, name, description);
           if (resultTuple is null)
           {
               return;
           }

           await Clients.Group(resultTuple.Value.server.Name).AddChannel(resultTuple.Value.channel.ChannelStub);
        }

        /// <summary>
        /// Delegates the Channel removal. Sends the removed channel stub to people in Server on success.
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="serverId"></param>
        public async Task RemoveChannel(string channelId, int serverId)
        {
            var resultTuple = await _channel.RemoveChannel(Context.UserIdentifier, serverId, channelId);
            if (resultTuple is null)
            {
                return;
            }

            await Clients.Group(resultTuple.Value.server.Name).RemoveChannel(resultTuple.Value.channel.ChannelStub);
        }
    }

    public interface IChannelHub
    {
        Task AddChannel(ChannelStub channel);
        Task RemoveChannel(ChannelStub channelStub);
    }
}