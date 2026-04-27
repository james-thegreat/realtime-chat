using RealTimeChat.Application.Abstractions;
using RealTimeChat.Domain.Models;

namespace RealTimeChat.Infrastructure.Persistence;

public class EfChatMessageRepository : IChatMessageRepository
{
    private readonly RealTimeChatDbContext _dbContext;

    public EfChatMessageRepository(RealTimeChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(ChatMessage message)
    {
        _dbContext.ChatMessages.Add(message);
        await _dbContext.SaveChangesAsync();
    }
}