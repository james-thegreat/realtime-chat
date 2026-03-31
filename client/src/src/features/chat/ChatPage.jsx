import { useEffect, useState } from "react";
import ChatHeader from "./components/ChatHeader";
import MessageInput from "./components/MessageInput";
import MessageList from "./components/MessageList";

function ChatPage() {
  const [username, setUsername] = useState("");
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    const savedUsername = localStorage.getItem("username");

    if (savedUsername) {
      setUsername(savedUsername);
    }
  }, []);

  useEffect(() => {
    localStorage.setItem("username", username);
  }, [username]);

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
      <ChatHeader />

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