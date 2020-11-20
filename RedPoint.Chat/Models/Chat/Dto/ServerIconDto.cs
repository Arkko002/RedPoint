using System.Drawing;

namespace RedPoint.Chat.Models.Chat.Dto
{
    /// <summary>
    /// Server DTO object that doesn't include internal server data.
    /// Should be used to transfer data necessary for displaying UI representation of a server.
    /// </summary>
    public class ServerIconDto : IDto
    {
        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }

        //Determines if server is visible in the server browser
        public bool IsVisible { get; set; }
    }
}