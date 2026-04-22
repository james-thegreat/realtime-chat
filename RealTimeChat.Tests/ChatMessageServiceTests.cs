using RealTimeChat.Application.Services;

namespace RealTimeChat.Tests;

public class ChatMessageServiceTests
{
    [Fact]
    public void Create_WithValidInput_ReturnsChatMessage()
    {
        // Arrange
        var service = new ChatMessageService();

        // Act
        var result = service.Create("James", "Hello world");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("James", result.UserName);
        Assert.Equal("Hello world", result.Text);
    }

    [Fact]
    public void Create_WithEmptyMessage_ThrowsException()
    {
        // Arrange
        var service = new ChatMessageService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            service.Create("James", "")
        );
    }

    [Fact]
    public void Create_WithWhitespaceMessage_ThrowsException()
    {
        // Arrange
        var service = new ChatMessageService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            service.Create("James", "   ")
        );
    }

    [Fact]
    public void Create_WithEmptyUserName_ThrowsException()
    {
        var service = new ChatMessageService();

        Assert.Throws<ArgumentException>(() => 
            service.Create("", "Hello world")
        );
    }

    [Fact]
    public void Create_WithWhitespaceUserName_ThrowsException()
    {
        var service = new ChatMessageService();

        Assert.Throws<ArgumentException>(() =>
            service.Create("   ", "Hello world")
        );
    }
}