using System;
using System.Text;

namespace RedPoint.Areas.Chat.Models
{
    public class UniqueIdentifier
    {
        public UniqueIdentifier()
        {
            Random random = new Random();
            int length = random.Next(4, 12);

            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            IdString = result.ToString();
        }

        public string IdString { get; set; }
    }
}