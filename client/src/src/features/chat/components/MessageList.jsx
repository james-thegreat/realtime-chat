import { useEffect, useRef } from "react";
import MessageItem from "./MessageItem";

function MessageList({ messages, currentUsername }) {
  const bottomRef = useRef(null);

  useEffect(() => {
    bottomRef.current?.scrollIntoView({
      behavior: "smooth",
    });
  }, [messages]);

  return (
    <section className="message-list">
      {messages.length === 0 ? (
        <p className="empty-messages">No messages yet.</p>
      ) : (
        <div>
          {messages.map((message) => {
            if (message.type === "system") {
              return (
                <p key={message.id} className="system-message">
                  {message.text}
                </p>
              );
            }

            return (
              <MessageItem
                key={message.id}
                message={message}
                isOwnMessage={message.userName === currentUsername}
              />
            );
          })}

          <div ref={bottomRef}></div>
        </div>
      )}
    </section>
  );
}

export default MessageList;