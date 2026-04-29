import * as signalR from "@microsoft/signalr";

export function createChatConnection(
  onMessageReceived,
  onErrorReceived,
  onTypingReceived,
  onSystemMessageReceived,
  onMessageHistoryReceived,
) {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${import.meta.env.VITE_API_BASE_URL}/chathub`)
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

  connection.on("ReceiveSystemMessage", (text) => {
    onSystemMessageReceived(text);
  });

  connection.on("ReceiveMessageHistory", (history) => {
    const safeHistory = history.map((message) => ({
      id: message.id || Date.now() + Math.random(),
      userName: message.userName,
      text: message.text,
      sentAtUtc: message.sentAtUtc,
    }));

    onMessageHistoryReceived(safeHistory);
  });

  return connection;
}