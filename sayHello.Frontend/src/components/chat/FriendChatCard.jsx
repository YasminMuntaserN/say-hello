import Image from "../../ui/Image"
import { BiCheck ,BiCheckDouble} from "react-icons/bi";
import { formatTime } from "../../utils/helpers";
import Box from "../../ui/Box";
import { useUser } from "../../context/UserContext";

function FriendChatCard({chatInfo}) {
    const {setUserInChat}=useUser();
    const {chatPartnerName ,chatPartnerImage, lastMessage, lastMessageStatus, lastMessageTime, unReadMessagesCount,isReceiver } = chatInfo;
    const IfRead = lastMessageStatus !== "Read";
  
    return (
      <Box colsNum={3} HandleOnClick={()=>setUserInChat(chatInfo)}>
        <div className="mt-3">
          <Image src={chatPartnerImage} alt={`${chatPartnerName}'s profile`} />
        </div>

        <div className="ml-[-30px]">
          <p className={StyledName}>{chatPartnerName}</p>
          <div className={StyledChatStatus}>
            {IfRead ? <BiCheck className={StyledIcon} /> : <BiCheckDouble className={StyledIcon} />}
            {lastMessage}
          </div>
        </div>
  
        <div>
          <p className={`${isReceiver ===1 ? "text-purple" :"text-lightText"}`}>
            {formatTime(lastMessageTime)}
          </p>
          {isReceiver ===1 && IfRead && (
            <p className={StyledCount}>
              {unReadMessagesCount}
            </p>
          )}
        </div>
        </Box>
    );
  }

const StyledCount ="h-5 w-5 rounded-full bg-gradient-btn text-sm flex text-white justify-center ml-5 mt-2";
const StyledChatStatus ="flex text-lightText font-light text-sm";
const StyledIcon =" text-lg mr-2";
const StyledName ="text-xl text-basic font-semibold group-hover:text-white";

export default FriendChatCard
