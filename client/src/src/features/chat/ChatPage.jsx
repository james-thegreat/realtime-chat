import { useEffect, useState } from "react";
import ChatHeader from "./components/ChatHeader";
import MessageInput from "./components/MessageInput";
import MessageList from "./components/MessageList";
import { createChatConnection } from "../../services/signalrConnection";

function ChatPage() {
  const [username, setUsername] = useState("");
  const [messages, setMessages] = useState([]);
  const [connectionStatus, setConnectionStatus] = useState("Disconnected");
  const connection = createChatConnection();


  useEffect(() => {
    const savedUsername = localStorage.getItem("username");

    if (savedUsername) {
      setUsername(savedUsername);
    }
  }, []);

  useEffect(() => {
    localStorage.setItem("username", username);
  }, [username]);
  
useEffect(() => {

  const startConnection = async () => {
    try {
      setConnectionStatus("Connecting");
      await connection.start();
      setConnectionStatus("Connected");
    } catch (error) {
      console.error("Failed to connect to SignalR:", error);
      setConnectionStatus("Disconnected");
    }
  };

  startConnection();

  connection.on("ReceiveMessage", (message) => {
    setMessages((prev) => [...prev, message]);
   });

  return () => {
    connection.stop();
  };
}, []);

  function handleSendMessage(text) {
    if (!username.trim()) {
      return;
    }

    const newMessage = {
      id: Date.now(),
      sender: username,
      text: text,
      sentAt: new Date().toLocaleTimeString(),
    };

  setMessages((currentMessages) => [...currentMessages, newMessage]); 
  }

  return (
    <main>
      <ChatHeader connectionStatus={connectionStatus} />

      <section>
        <h2>Your Name</h2>
        <input
          type="text"
          placeholder="Enter your name..."
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </section>

      <MessageList messages={messages} />
      <MessageInput onSend={handleSendMessage} />
    </main>
  );
}

export default ChatPage;