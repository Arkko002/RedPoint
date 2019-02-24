using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Data;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Hubs
{
    public class ChatSearchHub : Hub<IChatSearchHub>
    {
        private ApplicationDbContext _db;

        public ChatSearchHub(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Searches for given Message.Text in database
        /// </summary>
        /// <param name="text"></param>
        /// <param name="channel"></param>
        /// <param name="user"></param>
        public async Task Search(string text, ChannelStub channel, UserStub user)
        {
            //Message[] msgArr = _db.Messages.Where(m => (m.Text == text &&
            //                                            m.ChannelStub == channel &&
            //                                            m.UserStub == user)).ToArray();

            IQueryable<Message> q = _db.Messages;

            if (!(text is null))
            {
                q = q.Where(m => m.Text == text);
            }
            if ((channel is null))
            {
                q = q.Where(m => m.ChannelStub == channel);
            }

            if (!(user is null))
            {
                q = q.Where(m => m.UserStub == user);
            }

            var msgArr = q.ToArray();

            await Clients.Caller.ShowSearchResult(msgArr);
        }

        /// <summary>
        /// Returns UserStub list with UserStub.AppUserName containing given parameter
        /// </summary>
        /// <param name="nick"></param>
        public async Task UserAutocomplete(string nick)
        {
            var userList = _db.UserStubs.Where(u => u.AppUserName.StartsWith(nick)).ToList();

            await Clients.Caller.ShowUserAutocomplete(userList);
        }
    }

    public interface IChatSearchHub
    {
        Task ShowSearchResult(Message[] msgList);
        Task ShowUserAutocomplete(List<UserStub> userList);
    }
}
