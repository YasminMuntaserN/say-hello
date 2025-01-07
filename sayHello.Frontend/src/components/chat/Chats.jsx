import { useChat } from "../../context/UserContext"
import ChatPartner from "../ChatPartner/ChatPartner";
import Message from "../Messages/Message"
import Connections from "./Connections";
import Navbar from "./Navbar"

function Chats() {
  const { user :AccountUser, userInChat ,showChatPartnerOperations } = useChat();
  const chatRoom = userInChat?.from === "group"?
      `${userInChat?.type?.userId}_Room`
      :`${Math.min(userInChat?.type?.userId, AccountUser.userId)}_${Math.max(userInChat?.type?.userId, AccountUser.userId)}_Room`;
  const StyledContainer =`grid ${showChatPartnerOperations ?"grid-cols-[1fr_2fr_1fr]":"grid-cols-[1fr_2fr]"}`;
  return (
    <>
      <Navbar />
      <div className={StyledContainer}>
        <Connections />
        {userInChat?.type && <div><Message chatRoom={chatRoom} /></div>}
        {showChatPartnerOperations&& <ChatPartner />}
      </div>
    </>
  );
}



export default Chats;
