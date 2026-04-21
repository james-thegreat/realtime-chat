function TypingIdicator({ userName }) {
    if (!userName) {
        return null;
    }

    return <p>{userName} is Typing...</p>
}

export default TypingIdicator;