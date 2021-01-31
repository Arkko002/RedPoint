namespace RedPoint.Chat.Models.Chat.Dto
{
    /// <summary>
    /// Channel DTO that doesn't include internal channel data.
    /// Should be used to transfer data necessary for displaying UI representation of a channel.
    /// </summary>
    public class ChannelIconDto : IDto
    {
        public int Id { get; set; }
        
        public int ServerId { get; set; }
        public HubGroupIdentifier HubGroupId { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
    }
}