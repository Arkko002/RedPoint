using System.Threading.Tasks;
using RedPoint.Data;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Infrastructure.Builders
{
    public class ChannelBuilder
    {
        private ApplicationDbContext _db;

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
