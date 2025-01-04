import { BiSolidMessageSquareDots } from "react-icons/bi";
import { FaUsers } from "react-icons/fa";
import { MdBlockFlipped } from "react-icons/md";
import { FaArchive } from "react-icons/fa";
import { useAllArchivedUsers, useArchivedUserCount } from "../User/hooks/useArchivedUser";
import { useEffect } from "react";
import { useAllBlockedUsers, useBlockedUserCount } from "../User/hooks/useBlockedUser";
import { useChat } from "../../context/UserContext";
import CreateGroup from "../Groups/CreateGroup";
import { useAllGroups, useGroupsCount } from "../Groups/hooks/useGroups";

function QuickActions() {
  const{user ,updatedPartnerOperations ,setShowUsers ,setUsersToShow} =useChat();
  const {  ArchivedUser, ArchivedUsersCount } = useArchivedUserCount();
  const {  BlockedUser, BlockedUsersCount } = useBlockedUserCount();
  const { Group, GroupsCount }=useGroupsCount();
  const { mutate:getAllArchivedUsers, AllArchivedUsers } = useAllArchivedUsers();
  const { mutate:getAllBlockedUsers, AllBlockedUsers } = useAllBlockedUsers();
  const { mutate:getAllGroups , AllGroups } = useAllGroups();


  useEffect(() => {
    ArchivedUser(user.userId);
    BlockedUser(user.userId);
    Group(user.userId);
  },[ArchivedUser , BlockedUser ,Group,user.userId,updatedPartnerOperations]);

  const handleUsersToShow =(UsersType)=>{
      if(UsersType==="ArchivedUsers"){
        getAllArchivedUsers(user.userId);
        setUsersToShow(AllArchivedUsers);
        setShowUsers(pre=>!pre);
      }
      else if(UsersType==="BlockedUsers"){
        getAllBlockedUsers(user.userId);
        setUsersToShow(AllBlockedUsers);
        setShowUsers(pre=>!pre);
      }
      else if(UsersType==="AllGroups"){
        getAllGroups(user.userId);
        const mappedGroups = AllGroups.map((group) => ({
          username: group.chatPartnerName,
          profilePictureUrl: group.chatPartnerImage,
          userId: group.chatPartnerId,
        }));
        setUsersToShow(mappedGroups);
        setShowUsers(pre=>!pre);
      }
      else if(UsersType==="allPreviousChats"){
        setShowUsers(false);
      }
  }
  return (
    <div className={StyledContainer}>
      <BiSolidMessageSquareDots
        className={StyledIcon}
        onClick={()=>handleUsersToShow("allPreviousChats")}/>
      <div className="relative">
        <FaArchive
          onClick={()=>handleUsersToShow("ArchivedUsers")}
          className={`${StyledIcon} ${ArchivedUsersCount > 0 ? "text-purple" : ""}`}
        />
        {ArchivedUsersCount > 0 && (
          <span className={StyledCount}>{ArchivedUsersCount}</span>
        )}
      </div>
      <div className="relative">
        <MdBlockFlipped
          onClick={()=>handleUsersToShow("BlockedUsers")}
          className={`${StyledIcon} ${BlockedUsersCount > 0 ? "text-purple" : ""}`}
        />
        {BlockedUsersCount > 0 && (
          <span className={StyledCount}>{BlockedUsersCount}</span>
        )}
      </div>
      <div className="relative">
      <FaUsers 
            onClick={()=>handleUsersToShow("AllGroups")} 
            className={`${StyledIcon} ${GroupsCount > 0 ? "text-purple" : ""}`}
        />
        {GroupsCount > 0 && (
          <span className={StyledCount}>{GroupsCount}</span>
        )}
      </div>

      <CreateGroup />
    </div>
  );
}

const StyledContainer =
  "flex gap-10 justify-center p-5 rounded-lg shadow-2xl transition-all duration-300";
const StyledIcon = "text-3xl text-lightText hover:text-purple cursor-pointer";
const StyledCount =
  "absolute top-[-10px] right-[-10px] rounded-full bg-rose-700 flex items-center justify-center text-white w-5 h-5 text-sm";

export default QuickActions;