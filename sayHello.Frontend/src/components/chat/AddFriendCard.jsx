import { useState } from "react";
import { FaCheck } from "react-icons/fa";
import { useChat } from "../../context/UserContext";
import AddIcon from "../../ui/AddIcon";
import Box from "../../ui/Box";
import Image from "../../ui/Image";
import { useGroup } from "../../context/GroupContext";

function AddFriendCard({ user ,groupMember=false}) {
  console.log(user);
  const {setUserInChat ,setShowChatPartnerOperations}=useChat();
  const {addGroupMember }=useGroup();
  const { userId,username, profilePictureUrl ,bio} = user;
  const [groupMemberAdded , setGroupMemberAdded]=useState();

  const handleAddNewChat =()=>{
    setUserInChat(user);
    setShowChatPartnerOperations(pre=>!pre);
  };
  const handleAddNewMemberToGroup =()=>{
    addGroupMember(userId);
    setGroupMemberAdded(true);
  };
  const handleOnClick=()=>groupMember ? handleAddNewMemberToGroup() :handleAddNewChat() 
  return (
    <Box colsNum={3} HandleOnClick={()=>handleOnClick()}>
        <Image src={profilePictureUrl} alt={`${username}'s profile`} />

        <div className="ml-[-50px]  text-center">
          <p className={StyledName}>{username}</p>
          <p className="text-sm text-lightText">{bio?.length > 30 ? `${bio.substring(0, 30)}...` : bio}</p>
        </div>
        {groupMemberAdded && groupMember ?
        <p className="flex text-xl gap-5 mt-2"> <FaCheck className="text-green-700"/>Added</p>
        :<AddIcon size="lg"  handleOnClick={()=>handleOnClick() } />
        }
    </Box>
  );
}

const StyledName = "text-xl text-basic font-semibold group-hover:text-white";
export default AddFriendCard;