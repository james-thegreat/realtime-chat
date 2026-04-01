function ChatHeader({ connectionStatus }) {
  return (
    <header>
      <h1>Real-Time Chat</h1>
      <p>Build step by step with React, ASP.NET Core, and SignalR.</p>
      <p>Status: {connectionStatus}</p>
    </header>
  );
}

export default ChatHeader;