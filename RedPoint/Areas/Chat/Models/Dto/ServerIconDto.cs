using System.Drawing;

namespace RedPoint.Areas.Chat.Models.Dto
{
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