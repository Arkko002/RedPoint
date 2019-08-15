using System;
using System.Threading.Tasks;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Services.Builders
{
    public class MessageBuilder
    {
        private readonly ApplicationDbContext _db;

        public MessageBuilder(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Message> BuildMessage(UserDTO userDto, string text, Channel channel)
        {
            Message message = new Message()
            {
                UserDto = userDto,
                Text = text,
                DateTimePosted = DateTime.Now,
                ChannelStub = channel.ChannelStub                
            };

            try
            {
                _db.Messages.Add(message);
                channel.Messages.Add(message);
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return message;
        }
    }
}
