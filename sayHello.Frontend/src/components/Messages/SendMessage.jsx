import { useState } from "react";
import SendIcon from "../../ui/SendIcon";

function SendMessage({addMessage }) {
  const [message, setMessage] = useState("");

  const handleSend = () => {
    addMessage(message);
    setMessage("");
  };
  
  return (
    <div className={StyledContainer}>
      <input
        className={StyledInput}
        placeholder="Type a message here..."
        type="text"
        value={message}
        onChange={(e) => setMessage(e.target.value)}
        onKeyPress={(e) => e.key === 'Enter' && handleSend()}
      />
      <SendIcon handleOnClick={handleSend} />
    </div>
  );
}

export default SendMessage;
const StyledContainer ="bg-white shadow-2xl shadow-slate-500 transition-all p-7 flex justify-between items-center";
const StyledInput ="bg-gray rounded-3xl p-2 pl-7 w-full shadow-2xl transition-all duration-300 focus:outline-none";
