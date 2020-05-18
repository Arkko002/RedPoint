using System;
using System.Text;

namespace RedPoint.Areas.Chat.Models
{
    /// <summary>
    ///     Separate identifier used to prevent ID clashing in SignalR groups
    /// </summary>
    public class HubGroupIdentifier
    {
        public HubGroupIdentifier()
        {
            //TODO Maybe use a proper hashing algo here?

            var random = new Random();
            var length = 12;

            var characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
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