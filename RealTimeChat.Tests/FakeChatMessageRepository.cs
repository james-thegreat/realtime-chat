using RealTimeChat.Application.Abstractions;
using RealTimeChat.Domain.Models;

public class FakeChatMessageRepository : IChatMessageRepository
{
    public List<ChatMessage> SavedMessages { get; } = new();

    public Task SaveAsync(ChatMessage message)
    {
        SavedMessages.Add(message);
        return Task.CompletedTask;
    }
}