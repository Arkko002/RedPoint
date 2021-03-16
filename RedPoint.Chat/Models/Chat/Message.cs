using System;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat
{
    /// <summary>
    /// Represents a text message wrote in a channel.
    /// Contains a reference to <c>ChatUser</c> that sent it, the actual text of the message, and the <c>DateTime</c>.
    /// </summary>
    public class Message : IEntity
    {
        
        public int Id { get; set; }

        public DateTime DateTimePosted { get; set; }
        public string Text { get; set; }

        public ChatUser User { get; set; }
        
        public Channel Channel { get; }

        public Message() {}
        
        public Message(Channel channel)
        {
            Channel = channel;
        }

        public Message(MessageDto messageDto, ChatUser user)
        {
            DateTimePosted = DateTime.Parse(messageDto.DateTimePosted);
            Text = messageDto.Text;
            
            User = user;
        }

    }
}