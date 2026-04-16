function ErrorBanner({ message }) {
  if (!message) {
    return null;
  }

  return <p>{message}</p>;
}

export default ErrorBanner;