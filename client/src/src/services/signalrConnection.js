import * as signalR from "@microsoft/signalr";

export function createChatConnection() {
  return new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5044/chathub")
    .withAutomaticReconnect()
    .build();
}
