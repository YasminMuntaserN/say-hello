import { useChat } from "../../context/UserContext"
import Message from "../Messages/Message"
import Connections from "./Connections";
import Navbar from "./Navbar"

function Chats() {
  const { user :AccountUser, userInChat } = useChat();
  const chatRoom = `${Math.min(userInChat?.type?.userId, AccountUser.userId)}_${Math.max(userInChat?.type?.userId, AccountUser.userId)}_Room`;

  return (
    <>
      <Navbar />
      <div className={StyledContainer}>
        <Connections />
        {userInChat?.type && <div><Message chatRoom={chatRoom} user={userInChat}  receiverId={AccountUser?.userId}/></div>}
      </div>
    </>
  );
}


const StyledContainer ="grid grid-cols-[1fr_2fr]";
export default Chats;
