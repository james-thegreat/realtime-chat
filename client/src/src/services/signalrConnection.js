import * as signalR from "@microsoft/signalr";

export function createChatConnection(
  onMessageReceived,
  onErrorReceived,
  onTypingReceived,
) {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5044/chathub")
    .withAutomaticReconnect()
    .build();

  connection.on("ReceiveMessage", (message) => {
    const safeMessage = {
      id: message.id || Date.now() + Math.random(),
      userName: message.userName,
      text: message.text,
      sentAtUtc: message.sentAtUtc,
    };

    onMessageReceived(safeMessage);
  });

  connection.on("ReceiveError", (errorMessage) => {
    console.log("SignalR error:", errorMessage);
    onErrorReceived(errorMessage);
  });

  connection.on("ReceiveTyping", (userName) => {
    onTypingReceived(userName);
  });

  return connection;
}