import { useEffect, useRef, useState } from "react";
import ChatHeader from "./components/ChatHeader";
import MessageInput from "./components/MessageInput";
import MessageList from "./components/MessageList";
import { createChatConnection } from "../../services/signalrConnection";
import ErrorBanner from "./components/ErrorBanner";
import TypingIndicator from "./components/TypingIndicator";

function ChatPage() {
  const [username, setUsername] = useState("");
  const [messages, setMessages] = useState([]);
  const [connectionStatus, setConnectionStatus] = useState("Connecting");
  const connectionRef = useRef(null);
  const [errorMessage, setErrorMessage] = useState("");
  const [typingUser, setTypingUser] = useState("");
  const lastTypingSentRef = useRef(0);
  const typingUserRef = useRef("");

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
    const connection = createChatConnection(
      (message) => {
        setMessages((prev) => [...prev, message]);

        if (message.userName === typingUserRef.current) {
          setTypingUser("");
        }
      },
      (error) => {
        setErrorMessage(error);
      },
      (userName) => {
        console.log("typing received:", userName); // 👈 ADD THIS
        setTypingUser(userName);
      }
    );
    
    connectionRef.current = connection;

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
      connection.stop();
    };
  }, []);

  useEffect(() => {
    if (!typingUser) {
      return;
    }

    const timeoutId = setTimeout(() => {
      setTypingUser("");
    }, 2500);

    return () => clearTimeout(timeoutId);
  }, [typingUser]);

  useEffect(() => {
    if (!errorMessage) {
      return;
    }

    const timeoutId = setTimeout(() => {
      setErrorMessage("");
    }, 3000);

    return () => clearTimeout(timeoutId);
  }, [errorMessage]);

  useEffect(() => {
    typingUserRef.current = typingUser;
  }, [typingUser]);

  async function handleSendMessage(text) {
    setErrorMessage("");

    console.log("handleSendMessage fired");
    console.log("username:", username);
    console.log("text:", text);
    console.log("connection state:", connectionRef.current?.state);

    if (!username.trim() || !text.trim()) {
      setErrorMessage("Username and message text are required.");
      return;
    }

    if (!connectionRef.current || connectionStatus !== "Connected") {
      console.log("blocked: connection not ready");
      return;
    }

    const newMessage = {
      userName: username,
      text: text,
    };

    try {
      console.log("about to invoke SendMessage", newMessage);
      await connectionRef.current.invoke("SendMessage", newMessage);
      console.log("invoke succeeded");
    } catch (error) {
      console.error("invoke failed:", error);
    }
  }

  async function handleTyping() {
  const now = Date.now();

  // only send once every 1 second
  if (now - lastTypingSentRef.current < 1000) {
    return;
  }

  lastTypingSentRef.current = now;

  if (!username.trim()) {
    return;
  }

  if (!connectionRef.current || connectionStatus !== "Connected") {
    return;
  }

  try {
    await connectionRef.current.invoke("NotifyTyping", username);
  } catch (error) {
    console.error("typing notify failed:", error);
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
          onChange={(e) => {
            setUsername(e.target.value);
            setErrorMessage("");
          }}
        />
      </section>

      <ErrorBanner message={errorMessage} />
      <TypingIndicator userName={typingUser} />
      <MessageList messages={messages} />
      <MessageInput
        onSend={handleSendMessage}
        onTyping={() => {
          setErrorMessage("");
          handleTyping();
        }}
      />
    </main>
  );
}

export default ChatPage;