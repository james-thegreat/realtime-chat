import MessageItem from "./MessageItem";

function MessageList({ messages }) {
  return (
    <section>
      <h2>Messages</h2>

      {messages.length === 0 ? (
        <p>No messages yet.</p>
      ) : (
        <ul>
          {messages.map((message) => (
            <MessageItem key={message.id} message={message} />
          ))}
        </ul>
      )}
    </section>
  );
}

export default MessageList;