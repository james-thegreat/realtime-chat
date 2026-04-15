using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using RealTimeChat.Domain.Models;
using RealTimeChat.Application.Services;

namespace RealTimeChat.Api.Hubs;

public class ChatHub : Hub
{
    private readonly ChatMessageService _chatMessageService;

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
        Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(ChatMessage message)
    {
        if (string.IsNullOrWhiteSpace(message.UserName) ||
            string.IsNullOrWhiteSpace(message.Text))
        {
            return;
        }

        var chatMessage = _chatMessageService.Create(message.UserName, message.Text);

        Console.WriteLine($"Broadcasting message: UserName={chatMessage.UserName}, Text={chatMessage.Text}, SentAtUtc={chatMessage.SentAtUtc:o}");

        await Clients.All.SendAsync("ReceiveMessage", chatMessage);
    }
}