namespace RedPoint.Areas.Chat.Models
{
    public interface IUniqueIdentifier
    {
        int Identifier { get; set; }
        int GenerateIdentifier();
    }
}