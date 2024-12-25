import { BiSolidMessageSquareDots } from "react-icons/bi";
import { FaUsers } from "react-icons/fa";
import { MdBlockFlipped } from "react-icons/md";
import { FaArchive } from "react-icons/fa";
import { useAllArchivedUsers, useArchivedUserCount } from "../User/hooks/useArchivedUser";
import { useEffect } from "react";
import { useAllBlockedUsers, useBlockedUserCount } from "../User/hooks/useBlockedUser";
import { useUser } from "../../context/UserContext";

function QuickActions({setUsersToShown}) {
  const{user ,updatedPartnerOperations} =useUser();
  const {  ArchivedUser, ArchivedUsersCount } = useArchivedUserCount();
  const {  BlockedUser, BlockedUsersCount } = useBlockedUserCount();
const { mutate:getAllArchivedUsers, AllArchivedUsers } = useAllArchivedUsers();
const { mutate:getAllBlockedUsers, AllBlockedUsers } = useAllBlockedUsers();

  useEffect(() => {
    ArchivedUser(user.userId);
    BlockedUser(user.userId);
  },[ArchivedUser , BlockedUser ,user.userId,updatedPartnerOperations]);

  const handleUsersToShow =(UsersType)=>{
      if(UsersType==="ArchivedUsers"){
        getAllArchivedUsers(user.userId);
        setUsersToShown(AllArchivedUsers);
      }
      else if(UsersType==="BlockedUsers"){
        getAllBlockedUsers(user.userId);
        setUsersToShown(AllBlockedUsers);
      }
  }
  return (
    <div className={StyledContainer}>
      <BiSolidMessageSquareDots className={StyledIcon} />
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
      <FaUsers className={StyledIcon} />
    </div>
  );
}

const StyledContainer =
  "flex gap-10 justify-center p-5 rounded-lg shadow-2xl transition-all duration-300";
const StyledIcon = "text-3xl text-lightText hover:text-purple cursor:pointer";
const StyledCount =
  "absolute top-[-10px] right-[-10px] rounded-full bg-rose-700 flex items-center justify-center text-white w-5 h-5 text-sm";

export default QuickActions;