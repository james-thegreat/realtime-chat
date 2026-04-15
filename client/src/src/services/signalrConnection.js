import * as signalR from "@microsoft/signalr";

export function createChatConnection(onMessageReceived, onErrorReceived) {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5044/chathub")
    .withAutomaticReconnect()
    .build();

  connection.on("ReceiveMessage", (message) => {
    onMessageReceived(message);
  });

  connection.on("ReceiveError", (errorMessage) => {
    console.log("SignalR error:", errorMessage);
    onErrorReceived(errorMessage);
  });

  return connection;
}