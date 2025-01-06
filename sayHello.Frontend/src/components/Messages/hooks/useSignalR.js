import { useEffect, useRef, useState } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { useAllMessagesInChatRoom } from "./useAllMessagesInChatRoom";
import { useChat } from "../../../context/UserContext";

function addUniqueMessages(existingMessages, newMessages) {
  const existingIds = new Set(existingMessages.map((msg) => msg.messageId));
  return [
    ...existingMessages,
    ...newMessages.filter((msg) => !existingIds.has(msg.messageId)),
  ];
}

export function useSignalR(user, chatRoom, receiverId) {
  const { type, from } = user;
  const senderId = type.userId;
  const {
    mutate: RefetchMessages,
    isLoading,
    error: fetchError,
  } = useAllMessagesInChatRoom(from);

  const [messages, setMessages] = useState([]);
  const [error, setError] = useState(null);
  const [connecting, setConnecting] = useState(false);
  const connectionRef = useRef(null);
  const { setRefetchChats } = useChat();

  // Clear messages when chatRoom or receiverId changes
  useEffect(() => {
    setMessages([]);
  }, [chatRoom, receiverId]);

  useEffect(() => {
    const connect = async () => {
      try {
        // Disconnect from previous room if connected
        if (connectionRef.current) {
          await connectionRef.current.stop();
          connectionRef.current = null;
        }

        // Fetch messages for the new room/receiver
        await RefetchMessages(
          from === "group" ? senderId : { senderId, receiverId },
          {
            onSuccess: (data) => {
              setMessages(data);
            },
            onError: (err) => {
              console.error("Error fetching messages:", err);
              setError(err);
            },
          }
        );

        const conn = new HubConnectionBuilder()
          .withUrl("https://localhost:7201/chathub")
          .withAutomaticReconnect()
          .configureLogging(LogLevel.Information)
          .build();

        conn.on("ReceiveMessage", (message) => {
          console.log("Received message:", message);
          setMessages((prevMessages) =>
            addUniqueMessages(prevMessages, [message])
          );
          setRefetchChats(true);
          // setRefetchChats((pre) => !pre);
        });

        conn.on("UserJoinedRoom", (joinedSenderId) => {
          console.log(
            `User ${joinedSenderId} joined the chat room ${chatRoom}`
          );
        });

        await conn.start();
        connectionRef.current = conn;
        console.log("Connection established");
        await conn.invoke("JoinChatRoom", String(senderId), chatRoom);
        setConnecting(true);
      } catch (err) {
        setError(err);
        console.error("Connection failed:", err);
      }
    };

    connect();
    return () => {
      if (connectionRef.current) {
        connectionRef.current.off("ReceiveMessage");
        connectionRef.current.stop();
        setConnecting(false);
      }
    };
  }, [RefetchMessages, setRefetchChats, chatRoom, receiverId, senderId, from]);

  const sendMessage = async (message) => {
    if (connectionRef.current && message.trim()) {
      try {
        const sentMessage = await connectionRef.current.invoke(
          "SendMessage",
          chatRoom,
          {
            content: message,
            readStatus: "Read",
            senderId: from === "group" ? receiverId : senderId,
            receiverId: from !== "group" ? receiverId : null,
            groupId: from === "group" ? senderId : null,
          }
        );

        setMessages((prevMessages) =>
          addUniqueMessages(prevMessages, [sentMessage])
        );
      } catch (err) {
        setError(err);
        console.error("Send message failed:", err);
      }
    }
  };

  return {
    messages,
    error: error || fetchError,
    sendMessage,
    loading: connecting || isLoading,
  };
}
