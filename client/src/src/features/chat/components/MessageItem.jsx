function MessageItem({ message }) {
  return (
    <li>
      <div>
        <strong>{message.sender}</strong>
      </div>
      <div>{message.text}</div>
      <div>{message.sentAt}</div>
    </li>
  );
}

export default MessageItem;