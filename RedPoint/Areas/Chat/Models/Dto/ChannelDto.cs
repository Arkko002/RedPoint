namespace RedPoint.Areas.Chat.Models.Dto
{
    public class ChannelDto : IDto
    {
        public int Id { get; set; }
        public UniqueIdentifier UniqueId { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
    }
}
