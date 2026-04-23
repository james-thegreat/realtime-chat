using RealTimeChat.Application.Services;
using RealTimeChat.Application.Abstractions;
using RealTimeChat.Domain.Models;

namespace RealTimeChat.Tests;

public class ChatMessageServiceTests
{
    private readonly ChatMessageService _service;

    public ChatMessageServiceTests()
    {
        var fakeRepo = new FakeChatMessageRepository();
        _service = new ChatMessageService(fakeRepo);
    }

    [Fact]
    public async Task Create_WithValidInput_ReturnsChatMessage()
    {
        // Act
        var result = await _service.CreateAsync("James", "Hello world");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("James", result.UserName);
        Assert.Equal("Hello world", result.Text);
    }

    [Fact]
    public async Task Create_WithEmptyMessage_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("James", "")
        );
    }

    [Fact]
    public async Task Create_WithWhitespaceMessage_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("James", "   ")
        );
    }

    [Fact]
    public async Task Create_WithEmptyUserName_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("", "Hello world")
        );
    }

    [Fact]
    public async Task Create_WithWhitespaceUserName_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("   ", "Hello world")
        );
    }
}