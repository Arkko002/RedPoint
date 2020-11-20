using RedPoint.Data.Repository;

namespace RedPoint.Chat.Models.Chat.User_Settings
{
    /// <summary>
    ///     Container class for specialised settings classes
    /// </summary>
    public class UserSettings : IEntity
    {
        public int Id { get; set; }

        public PrivacySettings PrivacySettings { get; set; }
        public ChatSettings ChatSettings { get; set; }
    }
}