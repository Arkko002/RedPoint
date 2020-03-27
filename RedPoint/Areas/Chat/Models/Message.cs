using System;
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
    }
}