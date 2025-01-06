import { useEffect, useState} from "react";
import SendIcon from "../../ui/SendIcon";
import { useChat } from "../../context/UserContext";
import { AiOutlineStop } from "react-icons/ai";

function SendMessage({addMessage }) {
  const [message, setMessage] = useState('');
  const [isPrevent, setIsPrevent] = useState(false);

  const { checkIfUserPrevented, userInChat ,updatedPartnerOperations} = useChat();

  useEffect(()=>setIsPrevent(checkIfUserPrevented(
    userInChat?.from === 'group' ? 'group' : 'user',
    userInChat?.type.userId)
  ),[checkIfUserPrevented ,userInChat ,updatedPartnerOperations]);

  const handleSend = () => {
    const messageToSend = message;
    if (messageToSend.trim()) {
      addMessage(messageToSend);
      setMessage("");
    }
  };
  
  console.log(isPrevent);

  return (
    <div className={StyledContainer}>
    {isPrevent?
      <p className="pl-[30%]  text-red-700 font-semibold text-2xl flex gap-5 justify-center items-center"><AiOutlineStop/> You cannot send messages</p>
      :<>
      <input
        className={StyledInput}
        placeholder="Type a message here..."
        type="text"
        value={message}
        onChange={(e) => setMessage(e.target.value)}
        onKeyPress={(e) => e.key === 'Enter' && handleSend()}
      />
      <SendIcon handleOnClick={handleSend} />
      </>
    }
    </div>
  );
}

export default SendMessage;
const StyledContainer ="bg-white shadow-2xl shadow-slate-500 transition-all p-5 flex justify-between items-center";
const StyledInput ="bg-gray rounded-3xl p-2 pl-7 w-full shadow-2xl transition-all duration-300 focus:outline-none";
