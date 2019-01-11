using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Data;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Hubs
{
    public class ServerBrowserHub : Hub<IServerBrowserHub>
    {
        private ApplicationDbContext _db;

        public ServerBrowserHub(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns the list of all ServerStubs
        /// </summary>

        public void GetServerStubsList()
        {
            var servers = _db.ServerStubs.Where(s => s.IsVisible == true).ToArray();

            Clients.Caller.GetServerStubList(servers);
        }
    }

    public interface IServerBrowserHub
    {
        void GetServerStubList(ServerStub[] servers);
    }
}

