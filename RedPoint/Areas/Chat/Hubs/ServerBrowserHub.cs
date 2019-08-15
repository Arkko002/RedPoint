using System.Linq;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Hubs
{
    public class ServerBrowserHub : Hub<IServerBrowserHub>
    {
        private readonly ApplicationDbContext _db;

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

