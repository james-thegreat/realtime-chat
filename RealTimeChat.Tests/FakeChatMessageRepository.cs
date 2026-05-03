using RealTimeChat.Application.Abstractions;
using RealTimeChat.Domain.Models;
using System.Linq;

public class FakeChatMessageRepository : IChatMessageRepository
{
    public List<ChatMessage> SavedMessages { get; } = new();

    public Task SaveAsync(ChatMessage message)
    {
        SavedMessages.Add(message);
        return Task.CompletedTask;
    }

    public Task<List<ChatMessage>> GetRecentMessagesAsync(string roomName, int count)
    {
        var messages = SavedMessages
            .Where(m => m.RoomName == roomName)
            .OrderByDescending(m => m.SentAtUtc)
            .Take(count)
            .OrderBy(m => m.SentAtUtc)
            .ToList();

        return Task.FromResult(messages);
    }
}