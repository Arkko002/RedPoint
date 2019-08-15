using System.Threading.Tasks;
using RedPoint.Areas.Chat.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Services.Builders
{
    public class ChannelBuilder
    {
        private readonly ApplicationDbContext _db;

        public ChannelBuilder(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Channel> BuildChannel(string name, string description, Server server)
        {
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

            _db.Channels.Add(channel);
            server.Channels.Add(channel);
            channel.ChannelStub = channelStub;

            await _db.SaveChangesAsync();

            return channel;
        }
    }
}
