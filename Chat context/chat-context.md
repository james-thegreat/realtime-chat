# Real-Time Chat Project Context

## Project Goal

I am building a real-time chat system using:

* React (frontend)
* ASP.NET Core (backend)
* SignalR (real-time communication)

I want to learn properly, not copy code.

## Learning Requirements

* Explain design choices before code
* Keep steps small and incremental
* I will type code manually
* Focus on understanding architecture and responsibility

## Backend Architecture (already created)

server/

* RealTimeChat.Api → ASP.NET Core entry point (will host SignalR Hub)
* RealTimeChat.Application → business logic (chat use cases)
* RealTimeChat.Domain → core models (ChatMessage, etc.)
* RealTimeChat.Infrastructure → in-memory storage (repositories)

Dependencies:

* Api → Application, Infrastructure
* Application → Domain
* Infrastructure → Domain

## Frontend Architecture (already built)

client/src/

* app/
* components/
* features/chat/

  * ChatPage.jsx
  * components/

    * ChatHeader.jsx
    * MessageList.jsx
    * MessageItem.jsx
    * MessageInput.jsx
* services/

  * signalrConnection.js

## Current Frontend Features

* Local chat works (no backend yet)
* Messages stored in React state
* Message model includes:

  * id
  * sender
  * text
  * sentAt
* Username input with localStorage persistence
* Connection status UI ("Not connected")

## SignalR Status

* SignalR client package installed
* `signalrConnection.js` created with:

  * HubConnectionBuilder
  * URL: http://localhost:5000/chatHub
* Connection NOT yet started or used

## Current Goal (Next Step)

Integrate SignalR connection into React:

* Start connection using useEffect
* Update connection status:

  * Connecting
  * Connected
  * Disconnected
* Keep logic clean (use service file, not inline chaos)

## Important Rules

* Do NOT dump full solution
* One small step at a time
* Explain WHY before code
* Keep architecture clean (no mixing concerns)

## What I Already Understand

* React state and props
* Lifting state up
* Component separation
* Basic useEffect
* Folder-based architecture
* Backend layering (Api, Application, Domain, Infrastructure)

## What I Want Next

Guide me through:

1. Using SignalR connection in ChatPage
2. Managing connection lifecycle cleanly
3. Preparing for backend Hub integration
