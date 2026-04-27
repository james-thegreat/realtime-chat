using Microsoft.EntityFrameworkCore;
using RealTimeChat.Domain.Models;

namespace RealTimeChat.Infrastructure.Persistence;

public class RealTimeChatDbContext : DbContext
{
    public RealTimeChatDbContext(DbContextOptions<RealTimeChatDbContext> options)
        : base(options)
    {
    }

    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
}