function MessageItem({ message, isOwnMessage }) {
  return (
    <div className={`message-row ${isOwnMessage ? "own-message-row" : ""}`}>
      <div className={`message-bubble ${isOwnMessage ? "own-message-bubble" : ""}`}>
        <p className="message-user">{message.userName}</p>

        <p className="message-text">{message.text}</p>

        <p className="message-time">
          {message.sentAtUtc
            ? new Date(message.sentAtUtc).toLocaleTimeString([], {
                hour: "2-digit",
                minute: "2-digit",
              })
            : "—"}
        </p>
      </div>
    </div>
  );
}

export default MessageItem;