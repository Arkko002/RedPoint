using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RedPoint.Models.Chat_Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime DateTimePosted { get; set; }
        public string Text { get; set; }

        public UserStub UserStub { get; set; }
        public ChannelStub ChannelStub { get; set; }
    }
}