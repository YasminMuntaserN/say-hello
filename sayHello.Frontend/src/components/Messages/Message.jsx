import React from "react";
import { useSignalR } from "./hooks/useSignalR";
import MessageHeader from "./MessageHeader";
import OptionsMessages from "./OptionsMessages";
import SendMessage from "./SendMessage";
import { useChat } from "../../context/UserContext";
import ChatPartnerOperation from "../User/ChatPartnerOperation";


function Message({ user, chatRoom, receiverId }) {
  const { messages, error, sendMessage } = useSignalR(user.userId, chatRoom, receiverId);
  const {showChatPartnerOperations}=useChat();
  console.log(showChatPartnerOperations);
  if (error) {
    console.error("Error in Message component:", error);
    return <div className="text-red-500">Error: {error.message}</div>;
  }

  return (
    <div>
      <MessageHeader receiver={user} />
      {
        showChatPartnerOperations ?
          <>
      <div className="flex-grow overflow-y-auto h-[520px] relative">
        {messages.length > 0
          ? messages.map((msg) => (
              <div key={msg.messageId} className={`flex ${msg.receiverId === receiverId ? "justify-end" : "justify-start"} text-center`}>
                <div className={msg.receiverId === receiverId ?  StyledReceiverMessage :StyledSenderMessage}>
                  {msg.content}
                </div>
              </div>
            ))
          :
          <OptionsMessages />
        }
      </div>

      <SendMessage receiver={user} addMessage={sendMessage} />
      </>  
      :
      <ChatPartnerOperation />
      }     
    </div>
  );
}

const StyledSenderMessage = "bg-gray rounded-full m-3 p-3 w-fit text-lg px-5";  
const StyledReceiverMessage = "bg-gradient-message rounded-tl-full text-lg px-5 rounded-bl-full rounded-br-full border-t-5 border-l-5 border-b-5 border-r-0 m-3 p-3 w-fit text-white"; 



export default React.memo(Message);