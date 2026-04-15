using RealTimeChat.Domain.Models;

namespace RealTimeChat.Application.Services;

public class ChatMessageService
{
    public ChatMessage Create(string userName, string text)
    {
        if (string.IsNullOrWhiteSpace(userName) ||
            string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Username and text are required");
        }

        return new ChatMessage
        {
            UserName = userName,
            Text = text,
            SentAtUtc = DateTime.UtcNow
        };
    }
}