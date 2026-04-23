using RealTimeChat.Application.Abstractions;
using RealTimeChat.Domain.Models;

namespace RealTimeChat.Application.Services;

public class ChatMessageService
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatMessageService(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<ChatMessage> CreateAsync(string userName, string text)
    {
        if (string.IsNullOrWhiteSpace(userName) ||
            string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Username and text are required");
        }

        var chatMessage = new ChatMessage
        {
            UserName = userName,
            Text = text,
            SentAtUtc = DateTime.UtcNow
        };

        await _chatMessageRepository.SaveAsync(chatMessage);

        return chatMessage;
    }
}