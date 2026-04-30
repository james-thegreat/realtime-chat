using RealTimeChat.Domain.Models;

namespace RealTimeChat.Application.Abstractions;

public interface IChatMessageRepository
{
    Task SaveAsync(ChatMessage message);

    Task<List<ChatMessage>> GetRecentMessagesAsync(string roomName, int count);
}