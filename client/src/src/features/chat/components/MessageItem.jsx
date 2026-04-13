function MessageItem({ message }) {
  return (
    <li>
      <div>
        <strong>{message.userName}</strong>
      </div>
      <div>{message.text}</div>
      <div>
        {message.sentAtUtc
         ? new Date(message.sentAtUtc).toLocaleTimeString()
          : "—"}
      </div>
    </li>
  );
}

export default MessageItem;