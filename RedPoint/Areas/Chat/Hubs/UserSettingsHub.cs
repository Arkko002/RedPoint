using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Hubs
{
    public class UserSettingsHub : Hub<IUserSettingsHub>
    {
        private ApplicationDbContext _db;

        public UserSettingsHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task GetUserSettings()
        {
            await Clients.Caller.ShowUserSettings();
        }

    }

    public interface IUserSettingsHub
    {
        Task ShowUserSettings();
    }
}
