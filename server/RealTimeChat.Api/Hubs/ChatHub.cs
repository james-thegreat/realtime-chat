using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using RealTimeChat.Domain.Models;
using RealTimeChat.Application.Services;


namespace RealTimeChat.Api.Hubs;

public class ChatHub : Hub
{
    private readonly ChatMessageService _chatMessageService;
    private static readonly Dictionary<string, string> _connections = new();

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
        if (_connections.TryGetValue(Context.ConnectionId, out var userName))
        {
            _connections.Remove(Context.ConnectionId);

            Console.WriteLine($"{userName} left");

            await Clients.Others.SendAsync("ReceiveSystemMessage", $"{userName} left the chat");
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task NotifyTyping(string userName)
    {
        Console.WriteLine($"{userName} is typing...");
        await Clients.Others.SendAsync("ReceiveTyping", userName);
    }

    public async Task SendMessage(ChatMessage message)
    {
        try
        {
            var chatMessage = _chatMessageService.Create(message.UserName, message.Text);

            Console.WriteLine($"Broadcasting message: UserName={chatMessage.UserName}, Text={chatMessage.Text}, SentAtUtc={chatMessage.SentAtUtc:o}");

            await Clients.All.SendAsync("ReceiveMessage", chatMessage);
        }
        catch (ArgumentException)
        {
            await Clients.Caller.SendAsync("ReceiveError", "Username and message text are required.");
            return;
        }
    }

    public async Task JoinChat(string userName)
    {
        _connections[Context.ConnectionId] = userName;

        Console.WriteLine($"{userName} joined");

        await Clients.Others.SendAsync("ReceiveSystemMessage", $"{userName} joined the chat");
    }
}