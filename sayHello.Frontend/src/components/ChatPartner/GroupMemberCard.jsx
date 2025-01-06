import Image from "../../ui/Image";
import { PiChatCircleDotsLight } from "react-icons/pi";
import {useChat} from "../../context/UserContext";

function GroupMemberCard({member}) {
  const {username , userImg}=member;
  const {setUserInChat , setShowChatPartnerOperations}=useChat();
  console.log(member);
  const handleSetInChat=()=>{
    setShowChatPartnerOperations(false);
    setUserInChat(member);
  }
  return (
    <div className="flex w-full justify-between px-10  mb-2 py-2 ">
      <Image src={userImg} size="w-10 h-10" alt={`${username} image`}/>
      <p>{username}</p>
      <PiChatCircleDotsLight className="text-2xl cursor-pointer" onClick={()=>handleSetInChat()}/>
    </div>
  )
}

export default GroupMemberCard
