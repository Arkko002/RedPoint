using System;
using System.Text;

namespace RedPoint.Chat.Models
{
    //TODO Rework this completly, attach a unique identifier to every IEntity object, use proper hashing algorithm PLZ!!!
    /// <summary>
    ///     Separate identifier used to prevent ID clashing in SignalR groups
    /// </summary>
    public class HubGroupIdentifier
    {
        public HubGroupIdentifier()
        {
            //TODO Maybe use a proper hashing algo here?

            var random = new Random();
            const int length = 12;

            const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var result = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            IdString = result.ToString();
        }

        public string IdString { get; set; }
    }
}