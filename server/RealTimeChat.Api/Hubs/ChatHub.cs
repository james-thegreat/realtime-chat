using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RealTimeChat.Api.Hubs;

public class ChatHub : Hub
{
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

    // public async Task SendMessage(object message)
    // {
    //     await Clients.All.SendAsync("ReceiveMessage", message);
    // }

    public async Task SendMessage(object message)
    {
        Console.WriteLine("🔥 SendMessage hit");
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}