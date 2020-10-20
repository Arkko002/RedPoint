namespace RedPoint.Chat.Models.Dto
{
    public class ChannelIconDto : IDto
    {
        public int Id { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
    }
}