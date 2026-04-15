using RealTimeChat.Api.Models;

namespace RealTimeChat.Application.Services;

public class ChatMessageService
{
    public ChatMessage Create(string userName, string text)
    {
        return new ChatMessage
        {
            UserName = userName,
            Text = text,
            SentAtUtc = DateTime.UtcNow
        };
    }
}