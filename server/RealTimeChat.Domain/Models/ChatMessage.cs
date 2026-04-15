namespace RealTimeChat.Domain.Models;

public class ChatMessage
{
    public string UserName { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime SentAtUtc { get; set; }
}