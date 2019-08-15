using System;
using RedPoint.Areas.Identity.Models;

namespace RedPoint.Areas.Chat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime DateTimePosted { get; set; }
        public string Text { get; set; }

        public UserDTO UserDto { get; set; }
        public ChannelStub ChannelStub { get; set; }
    }
}