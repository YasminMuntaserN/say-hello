import React from "react";
import { useSignalR } from "./hooks/useSignalR";
import MessageHeader from "./MessageHeader";
import OptionsMessages from "./OptionsMessages";
import SendMessage from "./SendMessage";
import { formatTime } from "../../utils/helpers";
import { useChat } from "../../context/UserContext";


function Message({ chatRoom }) {
  const { user,userInChat}=useChat();
  const { messages, error, sendMessage } = useSignalR( chatRoom, user?.userId);
  const {type :receiver ,from } =userInChat;
  const IsReceiver=(msg)=>(msg.senderId === user?.userId) ;
  if (error) {
    console.error("Error in Message component:", error);
    return <div className="text-red-500">Error: {error.message}</div>;
  }
  console.log(messages);
  console.log(`${JSON.stringify(userInChat)} , ${JSON.stringify(user)}`);
  return (
    <div>
      <MessageHeader receiver={receiver} />
      {
      <>
      <div className="flex-grow overflow-y-auto h-[460px] relative">
        {messages.length > 0
          ? messages.map((msg) => (
              <div key={msg.messageId} className={`flex ${IsReceiver(msg)? "justify-end" : "justify-start"} text-center`}>
                <div className={IsReceiver(msg)?  StyledReceiverMessage :StyledSenderMessage}>
                <div className={messageHeader}>
                  <span>
                  ~{(IsReceiver(msg)?user.username : from==="group"? msg.senderName  :receiver.receiverName)  } 
                  </span>
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