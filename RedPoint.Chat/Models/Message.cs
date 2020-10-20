using System;
using RedPoint.Chat.Models.Dto;
using RedPoint.Data;

namespace RedPoint.Chat.Models
{
    public class Message : IEntity
    {
        public Message()
        {
        }

        public Message(MessageDto messageDto, ChatUser user)
        {
            DateTimePosted = DateTime.Parse(messageDto.DateTimePosted);
            Text = messageDto.Text;
            User = user;
        }

        public int Id { get; set; }

        public DateTime DateTimePosted { get; set; }
        public string Text { get; set; }

        public ChatUser User { get; set; }
    }
}