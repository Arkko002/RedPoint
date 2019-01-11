﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace RedPoint.Models.Chat_Models
{
    /// <summary>
    /// Server class that contains channel's list and user's list
    /// </summary>
    public class Server
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public List<UserStub> Users { get; set; }
        public List<Group> Groups { get; set; }
        public List<Channel> Channels { get; set; }

        public ServerStub ServerStub { get; set; }

        //Determines if server is visible in the server browser
        public bool IsVisible { get; set; }
    }

    /// <summary>
    /// DTO for Server
    /// </summary>
    public class ServerStub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        //Determines if server is visible in the server browser
        public bool IsVisible { get; set; }
    }
}