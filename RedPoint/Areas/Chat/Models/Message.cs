using System;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    public class Message : IEntity
    {
        public Message(MessageDto messageDto, ApplicationUser user)
        {
            DateTimePosted = DateTime.Parse(messageDto.DateTimePosted);
            Text = messageDto.Text;
            User = user;
        }

        public int Id { get; set; }

        public DateTime DateTimePosted { get; set; }
        public string Text { get; set; }

        public ApplicationUser User { get; set; }
    }
}