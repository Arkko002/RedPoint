﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RedPoint.Data;
using RedPoint.Infrastructure;
using RedPoint.Models.Users_Permissions_Models;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;
using System.Security.Claims;

namespace RedPoint.Hubs
{
    #if DEBUG
    #else
        [Authorize] 
    #endif
    public class ChatHub : Hub<IChatHub>
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;


        public ChatHub(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext, ApplicationDbContext db)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _db = db;
        }

        public void CheckIfUserLoggedIn()
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            if (user is null)
            {
                Clients.Caller.UserNotLoggedIn("/account/login");
            }
        }

        /// <summary>
        /// Server method called to add Message to database
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="channelId"></param>
        public void Send(string msg, int channelId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var channel = _db.Channels.Find(channelId);
            if (channel is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to write in nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                Clients.Caller.ChannelDoesntExist();
                return;
            }

            PermissionsManager permissionsManager = new PermissionsManager();
            if (!permissionsManager.CheckUserGroupsPermissions(user, channel, new[] { "CanWrite" }))
            {
                _logger.Warn("{0} (ID: {1}) tried to write in channel without write permission (Channel ID: {2))", user.UserName, user.Id, channelId);
                Clients.Caller.UserCantWrite();
                return;
            }

            if (user.UserStub is null)
            {
                var stubManager = new UserStubManager(_db);
                user.UserStub = stubManager.CreateUserStub(user);
            }

            Message message = new Message()
            {
                UserStub = user.UserStub,
                Text = msg,
                DateTimePosted = DateTime.Now
            };

            channel.Messages.Add(message);
            _db.SaveChanges();

            Clients.Group(channelId.ToString()).AddNewMessage(message);
        }

        /// <summary>
        /// Searches for given Message.Text in database
        /// </summary>
        /// <param name="text"></param>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public void Search(string text, ChannelStub channel, UserStub user)
        {
            Message[] msgArr = _db.Messages.Where(m => (m.Text == text &&
                                                        m.ChannelStub == channel &&
                                                        m.UserStub == user)).ToArray();

            //IQueryable<Message> q = _db.Messages;

            //if (!(text is null))
            //{
            //    q = q.Where(m => m.Text == text);
            //}
            //if ((channel is null))
            //{
            //    q = q.Where(m => m.ChannelStub == channel);
            //}

            //if (!(user is null))
            //{
            //    q = q.Where(m => m.UserStub == user);
            //}

            //var result = q.ToList();

            Clients.Caller.ShowSearchResult(msgArr);
        }

        /// <summary>
        /// Returns UserStub list with UserStub.AppUserName containing given parameter
        /// </summary>
        /// <param name="nick"></param>
        public void UserAutocomplete(string nick)
        {
            var userList = _db.UserStubs.Where(u => u.AppUserName.StartsWith(nick)).ToList();

            Clients.Caller.ShowUserAutocomplete(userList);
        }

        /// <summary>
        /// Gets last 50 Messages from Channel and sends them to Caller
        /// </summary>
        /// <param name="channelId"></param>
        public void ChannelChanged(int channelId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var channel = _db.Channels.Find(channelId);
            if (channel is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to join nonexistent channel (Channel ID: {2))", user.UserName, user.Id, channelId);
                Clients.Caller.ChannelDoesntExist();
                return;
            }

            Groups.RemoveFromGroupAsync(Context.ConnectionId, user.CurrentChannelId.ToString());
            user.CurrentChannelId = channelId;
            Groups.AddToGroupAsync(Context.ConnectionId, channelId.ToString());
            _db.SaveChanges();

            var lastMsgs = channel.Messages.Skip(Math.Max(0, channel.Messages.Count() - 50));

            Clients.Caller.GetMessagesFromDb(lastMsgs);
        }

        public void ServerChanged(int serverId)
        {
            ApplicationUser user =
                _userManager.FindByIdAsync(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;

            var server = _db.Servers.Find(serverId);
            if (server is null)
            {
                _logger.Warn("{0} (ID: {1}) tried to join nonexistent server (Server ID: {2))", user.UserName, user.Id, serverId);
                Clients.Caller.ServerDoesntExist();
                return;
            }

            var channels = server.Channels.ToList();

            Clients.Caller.GetChannnelList(channels);
        }
    }

    public interface IChatHub
    {
        void AddNewMessage(Message message);
        void ChannelDoesntExist();
        void GetChannnelList(List<Channel> channels);
        void GetMessagesFromDb(IEnumerable<Message> lastMsgs);
        void GetServerList(List<Server> list);
        void ServerDoesntExist();
        void ShowSearchResult(Message[] msgList);
        void ShowUserAutocomplete(List<UserStub> userList);
        void UserCantWrite();
        void UserNotLoggedIn(string v);
    }
}