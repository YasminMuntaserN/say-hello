import { useChat } from "../../context/UserContext";
import Image from "../../ui/Image";
import { HiDotsHorizontal } from "react-icons/hi";

function MessageHeader({receiver}) {
  const {receiverImage, receiverName, status  } = receiver;
  const { setShowChatPartnerOperations ,showChatPartnerOperations} =useChat();

  const statusColor= status=="Online"?"text-[#188D3F]":"text-lightTextColor";

  return (
    <div className={StyledContainer}>
        <div className={`${showChatPartnerOperations ?"ml-[-500px]":"ml-[-700px]"}`}>
          <Image src={receiverImage} alt={`${receiverImage}'s profile`} />
        </div>
        <div className="flex flex-col">
          <p className={StyledName}>{receiverName}</p>
          <p className={statusColor}>  {status} </p>
        </div>
        <button onClick={()=>setShowChatPartnerOperations(pre=>!pre)}><HiDotsHorizontal className="absolute right-10 top-5 text-2xl  text-lightText" /></button>
      </div>
  )
}
const StyledContainer = "relative flex gap-5 justify-center p-3  rounded-lg shadow-2xl transition-all duration-300";
const StyledName = "text-xl text-basic font-bold";
export default MessageHeader
