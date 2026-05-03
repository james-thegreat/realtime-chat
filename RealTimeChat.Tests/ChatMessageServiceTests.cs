using RealTimeChat.Application.Services;

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
        var result = await _service.CreateAsync("James", "Hello world", "general");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("James", result.UserName);
        Assert.Equal("Hello world", result.Text);
        Assert.Equal("general", result.RoomName);
    }

    [Fact]
    public async Task Create_WithEmptyMessage_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("James", "", "general")
        );
    }

    [Fact]
    public async Task Create_WithWhitespaceMessage_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("James", "   ", "general")
        );
    }

    [Fact]
    public async Task Create_WithEmptyUserName_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("", "Hello world", "general")
        );
    }

    [Fact]
    public async Task Create_WithWhitespaceUserName_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("   ", "Hello world", "general")
        );
    }

    [Fact]
    public async Task Create_WithEmptyRoomName_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("James", "Hello world", "")
        );
    }

    [Fact]
    public async Task Create_WithWhitespaceRoomName_ThrowsException()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.CreateAsync("James", "Hello world", "   ")
        );
    }
}