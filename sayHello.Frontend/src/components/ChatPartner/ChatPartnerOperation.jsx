import { MdBlockFlipped } from "react-icons/md";
import { FaArchive } from "react-icons/fa";
import { TbLockOpen ,TbArchiveOff} from "react-icons/tb";
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";
import { useChat } from "../../context/UserContext";
import { useArchivedUser ,useDeleteArchivedUser,useIsArchivedUser } from "../User/hooks/useArchivedUser";
  import { useBlockedUser ,useIsBlockedUser, useDeleteBlockedUser} from "../User/hooks/useBlockedUser";



function ChatPartnerOperation() {
  const {setUpdatedPartnerOperations, setShowChatPartnerOperations, userInChat
        , user ,handlePreventSendMessage ,removePreventedUser } = useChat();
  const { ArchivedUser, isLoading: isArchiving, error: archiveError } = useArchivedUser();
  const { BlockedUser, isLoading: isBlocking, error: blockError } = useBlockedUser();
  const { unArchivedUser, isLoading: isUnArchiving, error: UnArchiveError } = useDeleteArchivedUser();
  const { unBlockedUser, isLoading: isUnBlocking, error: UnBlockError } = useDeleteBlockedUser();
  const { isArchived } = useIsArchivedUser(userInChat?.type.userId ,user.userId );
  const { isBlocked } = useIsBlockedUser( userInChat?.type.userId ,user.userId );
  
  console.log(userInChat?.type.userId, user.userId);
  console.log(isArchived, isBlocked);

  const handleOperation = (operation) => {
    operation(operation===ArchivedUser?
      {
        userId: user.userId,
        archivedUserId: userInChat.type.userId,
        dateArchived: new Date().toISOString(),
      }:
      {
        userId: user.userId,
        blockedByUserId: userInChat.type.userId,
        dateBlocked: new Date().toISOString(),
      }
      ,
      {
        onSuccess: (data) => {
          console.log("Operation completed successfully:", data);
          setShowChatPartnerOperations((prev) => !prev);
          setUpdatedPartnerOperations(operation===BlockedUser?"BlockedUsers" :"ArchivedUsers");
          operation===BlockedUser && handlePreventSendMessage({name:"user" ,id:userInChat?.type.userId});
        },
      }
    );
  };
  const handleRemoveOperation = (operation) => {
    operation(operation==unArchivedUser ?
      {
        ArchivedUserId: userInChat.type.userId,
        ArchivedByUserId: user.userId,
      }:
      {
        BlockedUserId: userInChat.type.userId,
        BlockedByUserId: user.userId,
      }
      ,
      {
        onSuccess: (data) => {
          console.log("Operation completed successfully:", data);
          setShowChatPartnerOperations((prev) => !prev);
          setUpdatedPartnerOperations(operation===BlockedUser?"BlockedUsers" :"ArchivedUsers");
          operation === unBlockedUser && removePreventedUser("user" ,userInChat?.type.userId);
        },
      }
    );
  };

  if (archiveError || blockError ||UnArchiveError ||UnBlockError) {
    console.error("Error in ChatPartnerOperation component:", archiveError || blockError);
    return <div className="text-red-500">Error: {(archiveError || blockError).message}</div>;
  }

  return (
    <>
      {isArchiving || isBlocking ||isUnArchiving || isUnBlocking? (
        <SpinnerMini />
      ) : (
        <div className={StyledContainer}>
          {isBlocked ?
          <div className={StyledRemoveOption} onClick={() => handleRemoveOperation(unBlockedUser)}>
            <TbLockOpen /> UnBlock User
          </div>
          :
          <div className={StyledOption} onClick={() => handleOperation(BlockedUser)}>
            <MdBlockFlipped /> Block User
          </div>
          }
        {isArchived ?
          <div className={StyledRemoveOption} onClick={() => handleRemoveOperation(unArchivedUser)}>
            <TbArchiveOff /> UnArchive User
          </div>
          :
          <div className={StyledOption} onClick={() => handleOperation(ArchivedUser)}>
            <FaArchive /> Archive User
          </div>
          }
          <Button variant="full" onClick={() => setShowChatPartnerOperations((prev) => !prev)}>
            Cancel
          </Button>
        </div>
      )}
    </>
  );
}

export default ChatPartnerOperation;

const StyledContainer = "flex justify-between flex-col p-7";
const StyledOption = "flex gap-5 justify-center items-center w-full rounded-xl py-5 mb-2 text-xl hover:bg-rose-200 text-rose-700 font-semibold";
const StyledRemoveOption = "flex gap-5 justify-center items-center w-full rounded-xl py-5 mb-2 text-xl hover:bg-[#054014]  text-[#89F29F] font-semibold";
