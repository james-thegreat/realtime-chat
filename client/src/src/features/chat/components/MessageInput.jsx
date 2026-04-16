import { useState } from "react";

function MessageInput({ onSend, onTyping }) {
  const [text, setText] = useState("");

  function handleSubmit(event) {
    event.preventDefault();

    const trimmedText = text.trim();

    if (!trimmedText) return;

    onSend(trimmedText);
    setText("");
  }

  return (
    <section>
      <h2>Send a message</h2>

      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Type a message..."
          value={text}
          onChange={(e) => {
            setText(e.target.value);
            onTyping();
          }}
        />
        <button type="submit" disabled={!text.trim()}>
          Send
        </button>
      </form>
    </section>
  );
}

export default MessageInput;