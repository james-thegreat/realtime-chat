using RealTimeChat.Application.Abstractions;
using RealTimeChat.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<ChatMessage>> GetRecentMessagesAsync(string roomName, int count)
    {
        return await _dbContext.ChatMessages
            .Where(message => message.RoomName == roomName)
            .OrderByDescending(message => message.SentAtUtc)
            .Take(count)
            .OrderBy(message => message.SentAtUtc)
            .ToListAsync();
    }
}