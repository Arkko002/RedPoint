using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RedPoint.Data;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using RedPoint.Models.Users_Permissions_Models;

namespace RedPoint.Infrastructure.Builders
{
    public class MessageBuilder
    {
        private ApplicationDbContext _db;

        public MessageBuilder(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Message> BuildMessage(UserStub userStub, string text, Channel channel)
        {
            Message message = new Message()
            {
                UserStub = userStub,
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
