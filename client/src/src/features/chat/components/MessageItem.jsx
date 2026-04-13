function MessageItem({ message }) {
  return (
    <li>
      <div>
        <strong>{message.userName}</strong>
      </div>
      <div>{message.text}</div>
      <div>{message.sentAtUtc}</div>
    </li>
  );
}

export default MessageItem;