import React from "react";
import { useSignalR } from "./hooks/useSignalR";
import MessageHeader from "./MessageHeader";
import OptionsMessages from "./OptionsMessages";
import SendMessage from "./SendMessage";
import { formatTime } from "../../utils/helpers";


function Message({ user, chatRoom, receiverId }) {
  const { messages, error, sendMessage } = useSignalR(user, chatRoom, receiverId);
  const {type :receiver } =user;
const IsReceiver=(msg)=>(msg.senderId === receiverId) ;
  if (error) {
    console.error("Error in Message component:", error);
    return <div className="text-red-500">Error: {error.message}</div>;
  }
  
  return (
    <div>
      <MessageHeader receiver={receiver} />
      {
      <>
      <div className="flex-grow overflow-y-auto h-[500px] relative">
        {messages.length > 0
          ? messages.map((msg) => (
              <div key={msg.messageId} className={`flex ${IsReceiver(msg)? "justify-end" : "justify-start"} text-center`}>
                <div className={IsReceiver(msg)?  StyledReceiverMessage :StyledSenderMessage}>
                <div className={messageHeader}>
                  <span>~{IsReceiver(msg)?  msg.senderName :msg.senderName } </span>
                  <span>{formatTime(msg?.sendDT )} </span>
                </div>
                  {msg.content}
                </div>
              </div>
            ))
          :
          <OptionsMessages addMessage={sendMessage}/>
        }
      </div>
      <SendMessage receiver={receiver} addMessage={sendMessage} />
      </>  
      }     
    </div>
  );
}

const StyledSenderMessage = "bg-gray rounded-full m-3 p-2 w-fit text-lg px-5";  
const StyledReceiverMessage = "bg-gradient-message p-2 rounded-tl-full text-lg px-5 rounded-bl-full rounded-br-full border-t-5 border-l-5 border-b-5 border-r-0 m-3 w-fit text-white"; 
const messageHeader ="flex justify-between text-blue gap-10 text-sm";


export default React.memo(Message);