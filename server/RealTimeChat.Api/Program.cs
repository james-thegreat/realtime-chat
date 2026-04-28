using RealTimeChat.Api.Hubs;
using RealTimeChat.Application.Services;
using Microsoft.EntityFrameworkCore;
using RealTimeChat.Application.Abstractions;
using RealTimeChat.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173",
                "https://happy-sea-0ddee0400.7.azurestaticapps.net"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

builder.Services.AddDbContext<RealTimeChatDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IChatMessageRepository, EfChatMessageRepository>();
builder.Services.AddScoped<ChatMessageService>();

var app = builder.Build();

// 🔥 Ensure database is created on startup (important for Azure)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RealTimeChatDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.MapGet("/health", () => Results.Ok("API is running"));

app.MapHub<ChatHub>("/chathub");

app.Run();