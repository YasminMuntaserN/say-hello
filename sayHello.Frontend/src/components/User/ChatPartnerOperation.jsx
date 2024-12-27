import { MdBlockFlipped } from "react-icons/md";
import { FaArchive } from "react-icons/fa";
import { TbLockOpen ,TbArchiveOff} from "react-icons/tb";
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";
import { useChat } from "../../context/UserContext";
import { useArchivedUser ,useDeleteArchivedUser,useIsArchivedUser } from "./hooks/useArchivedUser";
  import { useBlockedUser ,useIsBlockedUser, useDeleteBlockedUser} from "./hooks/useBlockedUser";



function ChatPartnerOperation() {
  const {setUpdatedPartnerOperations, setShowChatPartnerOperations, userInChat, user } = useChat();
  const { ArchivedUser, isLoading: isArchiving, error: archiveError } = useArchivedUser();
  const { BlockedUser, isLoading: isBlocking, error: blockError } = useBlockedUser();
  const { unArchivedUser, isLoading: isUnArchiving, error: UnArchiveError } = useDeleteArchivedUser();
  const { unBlockedUser, isLoading: isUnBlocking, error: UnBlockError } = useDeleteBlockedUser();
  const { isArchived } = useIsArchivedUser(userInChat.userId, user.userId);
  const { isBlocked } = useIsBlockedUser(userInChat.userId, user.userId);
  
  console.log(`isBlocked ${isBlocked},isArchived ${isArchived}`);

  const handleOperation = (operation) => {
    operation(operation==ArchivedUser?
      {
        userId: user.userId,
        archivedUserId: userInChat.userId,
        dateArchived: new Date().toISOString(),
      }:
      {
        userId: user.userId,
        blockedByUserId: userInChat.userId,
        dateBlocked: new Date().toISOString(),
      }
      ,
      {
        onSuccess: (data) => {
          console.log("Operation completed successfully:", data);
          setShowChatPartnerOperations((prev) => !prev);
          setUpdatedPartnerOperations(true);s
        },
      }
    );
  };
  const handleRemoveOperation = (operation) => {
    operation(operation==unArchivedUser ?
      {
        ArchivedUserId: userInChat.userId,
        ArchivedByUserId: user.userId,
      }:
      {
        BlockedUserId: userInChat.userId,
        BlockedByUserId: user.userId,
      }
      ,
      {
        onSuccess: (data) => {
          console.log("Operation completed successfully:", data);
          setShowChatPartnerOperations((prev) => !prev);
          setUpdatedPartnerOperations(true);
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

const StyledContainer = "flex justify-between items-center flex-col p-20";
const StyledOption = "flex gap-5 justify-center items-center w-full rounded-xl py-5 m-2 text-xl hover:bg-rose-200 text-rose-700 font-semibold";
const StyledRemoveOption = "flex gap-5 justify-center items-center w-full rounded-xl py-5 m-2 text-xl hover:bg-[#054014]  text-[#89F29F] font-semibold";
