import { useEffect, useRef, useState } from "react";
import ChatHeader from "./components/ChatHeader";
import MessageInput from "./components/MessageInput";
import MessageList from "./components/MessageList";
import { createChatConnection } from "../../services/signalrConnection";

function ChatPage() {
  const [username, setUsername] = useState("");
  const [messages, setMessages] = useState([]);
  const [connectionStatus, setConnectionStatus] = useState("Connecting");
  const connectionRef = useRef(null);

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
    const connection = createChatConnection();
    connectionRef.current = connection;

    connection.on("ReceiveMessage", (message) => {
      console.log("Received from hub:", message);

      const safeMessage = {
        ...message,
        id: message.id || Date.now() + Math.random(),
      };

      setMessages((prev) => [...prev, safeMessage]);
    });

    const startConnection = async () => {
      try {
        console.log("Starting SignalR connection...");
        await connection.start();
        console.log("SignalR connected");
        setConnectionStatus("Connected");
      } catch (error) {
        console.error("Failed to connect to SignalR:", error);
        setConnectionStatus("Disconnected");
      }
    };

    startConnection();

    return () => {
      connection.off("ReceiveMessage");
      connection.stop();
    };
  }, []);

  async function handleSendMessage(text) {
    console.log("handleSendMessage fired");
    console.log("username:", username);
    console.log("text:", text);
    console.log("connection state:", connectionRef.current?.state);

    if (!username.trim() || !text.trim()) {
      console.log("blocked by validation");
      return;
    }

    if (!connectionRef.current || connectionStatus !== "Connected") {
      console.log("blocked: connection not ready");
      return;
    }

    const newMessage = {
      id: Date.now(),
      sender: username,
      text: text,
      sentAt: new Date().toLocaleTimeString(),
    };

    try {
      console.log("about to invoke SendMessage", newMessage);
      await connectionRef.current.invoke("SendMessage", newMessage);
      console.log("invoke succeeded");
    } catch (error) {
      console.error("invoke failed:", error);
    }
  }

  console.log("Messages state:", messages);

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