namespace RealTimeChat.Domain.Models;

public class ChatMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserName { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTime SentAtUtc { get; set; }
}