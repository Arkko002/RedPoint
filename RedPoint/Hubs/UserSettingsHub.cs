using System.Threading.Tasks;
using RedPoint.Data;
using Microsoft.AspNetCore.SignalR;

namespace RedPoint.Hubs
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
