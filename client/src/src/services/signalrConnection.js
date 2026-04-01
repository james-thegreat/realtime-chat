import * as signalR from "@microsoft/signalr";

export function createChatConnection() {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/chatHub")
    .withAutomaticReconnect()
    .build();

  return connection;
}
