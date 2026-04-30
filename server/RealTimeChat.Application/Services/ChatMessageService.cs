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

    public async Task<ChatMessage> CreateAsync(string userName, string text, string roomName)
    {
        if (string.IsNullOrWhiteSpace(userName) ||
            string.IsNullOrWhiteSpace(text) ||
            string.IsNullOrWhiteSpace(roomName))
        {
            throw new ArgumentException("Username, text, and room name are required");
        }

        var chatMessage = new ChatMessage
        {
            UserName = userName,
            Text = text,
            RoomName = roomName,
            SentAtUtc = DateTime.UtcNow
        };

        await _chatMessageRepository.SaveAsync(chatMessage);

        return chatMessage;
    }

    public async Task<List<ChatMessage>> GetRecentMessagesAsync(string roomName, int count)
    {
        if (string.IsNullOrWhiteSpace(roomName))
        {
            throw new ArgumentException("Room name is required");
        }

        return await _chatMessageRepository.GetRecentMessagesAsync(roomName, count);
    }
}