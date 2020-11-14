namespace RedPoint.Chat.Models.Dto
{
    /// <summary>
    /// Message DTO object that doesn't include user's account data in it.
    /// Use provided user ID to retrieve full account object.
    /// </summary>
    public class MessageDto : IDto
    {
        public int Id { get; set; }

        public string DateTimePosted { get; set; }
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}