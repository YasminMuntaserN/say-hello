import { useUser } from "../../context/UserContext"
import Connections from "./Connections"
import Message from "../Messages/Message"
import Navbar from "./Navbar"

function Chats() {
  const {userInChat , user: AccountUser }= useUser();
  const chatRoom = `${Math.min(userInChat?.userId, AccountUser.userId)}_${Math.max(userInChat?.userId, AccountUser.userId)}_Room`;

  return (
    <>
      <Navbar />
      <div className={StyledContainer}>
        <Connections />
        {userInChat && <div><Message chatRoom={chatRoom} user={userInChat}  receiverId={AccountUser?.userId}/></div>}
      </div>
    </>
  );
}


const StyledContainer ="grid grid-cols-[1fr_2fr]";
export default Chats;
