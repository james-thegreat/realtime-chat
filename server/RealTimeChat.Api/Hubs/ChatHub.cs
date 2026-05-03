using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using RealTimeChat.Domain.Models;
using RealTimeChat.Application.Services;


namespace RealTimeChat.Api.Hubs;

public class ChatHub : Hub
{
    private readonly ChatMessageService _chatMessageService;

    private static readonly Dictionary<string, UserConnection> _connections = new();

    private record UserConnection(string UserName, string RoomName);

    public ChatHub(ChatMessageService chatMessageService)
    {
        _chatMessageService = chatMessageService;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (_connections.TryGetValue(Context.ConnectionId, out var connection))
        {
            _connections.Remove(Context.ConnectionId);

            Console.WriteLine($"{connection.UserName} left room {connection.RoomName}");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.RoomName);

            await Clients
                .Group(connection.RoomName)
                .SendAsync("ReceiveSystemMessage", $"{connection.UserName} left {connection.RoomName}");
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task NotifyTyping(string userName, string roomName)
    {
        Console.WriteLine($"{userName} is typing in {roomName}...");

        await Clients
            .OthersInGroup(roomName)
            .SendAsync("ReceiveTyping", userName);
    }

    public async Task SendMessage(ChatMessage message)
    {
        try
        {
            var chatMessage = await _chatMessageService.CreateAsync(
                message.UserName,
                message.Text,
                message.RoomName
            );

            Console.WriteLine($"Broadcasting message: UserName={chatMessage.UserName}, Text={chatMessage.Text}, Room={message.RoomName}, SentAtUtc={chatMessage.SentAtUtc:o}");

            await Clients
                .Group(message.RoomName)
                .SendAsync("ReceiveMessage", chatMessage);
        }
        catch (ArgumentException)
        {
            await Clients.Caller.SendAsync("ReceiveError", "Username and message text are required.");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            await Clients.Caller.SendAsync("ReceiveError", "Server error while sending message.");
        }
    }

    public async Task JoinChat(string userName, string roomName)
    {
        _connections[Context.ConnectionId] = new UserConnection(userName, roomName);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        Console.WriteLine($"{userName} joined room {roomName}");

        var recentMessages = await _chatMessageService.GetRecentMessagesAsync(roomName, 50);

        await Clients.Caller.SendAsync("ReceiveMessageHistory", recentMessages);

        await Clients
            .Group(roomName)
            .SendAsync("ReceiveSystemMessage", $"{userName} joined {roomName}");
    }
}