namespace RedPoint.Areas.Chat.Models.Dto
{
    public class ServerDto : IDto
    {
        public int Id { get; set; }
        public UniqueIdentifier UniqueId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        //Determines if server is visible in the server browser
        public bool IsVisible { get; set; }
    }
}
