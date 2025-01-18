import { FaSignOutAlt } from "react-icons/fa";
import GroupMembers from "./GroupMembers";
import { useState } from "react";
import SpinnerMini from "../../ui/SpinnerMini";
import { useLeaveGroup } from "../Groups/hooks/useGroupMember";
import { useChat } from "../../context/UserContext";
import CreateGroup from "../Groups/CreateGroup";
import { useGroup } from "../../context/GroupContext";

function ChatGroupOperation({groupId}) {
  const[groupMemberId , setGroupMemberId] = useState(null);
  const[groupMembers , setGroupMembers] = useState([]);
  const {setGroupJoinedOrLeft}=useGroup(); 
  const {setShowChatPartnerOperations ,handlePreventSendMessage } =useChat();
  const { mutate :LeaveGroup, isLoading :loadingLeaveGroup, error:ErrorLeaveGroup } =useLeaveGroup();

  const handleDeleteGroupMember=()=>{
    LeaveGroup(groupMemberId ,{
      onSuccess : ()=>{
        setShowChatPartnerOperations(false);
        handlePreventSendMessage({name:"group" ,id:groupId});
        setGroupJoinedOrLeft(true);

  }});
  }


  if (ErrorLeaveGroup) {
    return <p>Error preforming operation</p>;
  }
  return (
    <>
        <div className="flex justify-between px-20 pb-5">
          {loadingLeaveGroup ? <SpinnerMini/> : <FaSignOutAlt className={StyledOption} onClick={()=>handleDeleteGroupMember()}/> }
          <CreateGroup groupInfo={{groupId , groupMembers}}/>
        </div>
        <GroupMembers setGroupMemberId={setGroupMemberId} groupId={groupId} setGroupMembers={setGroupMembers}/>
    </>
  )
}
const StyledOption ="text-3xl text-lightText hover:text-purple cursor-pointer";
// const StyledAddOption ="flex gap-7 justify-center items-center w-full rounded-xl py-3 mb-2 text-xl hover:bg-[#054014]  text-[#3b7448]  font-semibold";

export default ChatGroupOperation
