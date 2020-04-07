using System;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Models
{
    public class Message : IEntity
    {
        public int Id { get; set; }
        public DateTime DateTimePosted { get; set; }
        public string Text { get; set; }

        public ApplicationUser User { get; set; }

        public Message(MessageDto messageDto, ApplicationUser user)
        {
            DateTimePosted = DateTime.Parse(messageDto.DateTimePosted);
            Text = messageDto.Text;
            User = user;
        }
    }
}