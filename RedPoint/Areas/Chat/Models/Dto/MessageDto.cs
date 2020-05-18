namespace RedPoint.Areas.Chat.Models.Dto
{
    public class MessageDto : IDto
    {
        public int Id { get; set; }

        public string DateTimePosted { get; set; }
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}