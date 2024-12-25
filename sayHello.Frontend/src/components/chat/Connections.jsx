import { useEffect, useState } from "react";
import { useUser } from "../../context/UserContext";
import { useAllUsersBySenderId } from  "../User/hooks/useAllUsersBySenderId";
import { useAllUsers } from  "../User/hooks/useAllUsers";
import SearchBar from "../../ui/SearchBar"
import QuickActions from "./QuickActions"
import FriendChatCard from "./FriendChatCard";
import AddFriend from "./AddFriend";
import AddFriendCard from "./AddFriendCard";

function Connections() {
  const { user, showUsers ,refetchChats ,updatedPartnerOperations} = useUser();
  const { mutate, isLoading, error, AllUsers: AllUsersBySenderId } = useAllUsersBySenderId();
  const { isLoading: LoadingAllUsers, error: ErrorAllUser, AllUsers } = useAllUsers();
  const [usersToShown , setUsersToShown] =useState(AllUsers);
  useEffect(() => {
    if (user) {
      mutate(user.userId);
    }
  }, [mutate, user,refetchChats,updatedPartnerOperations]);
  

  return (
    <div className="bg-[#f8fafc] h-screen flex flex-col">
      <QuickActions setUsersToShown={setUsersToShown}/>
      <AddFriend />
      <SearchBar />
      {isLoading && <p>Loading messages...</p>}
      {error && <p>Error fetching messages: {error.message}</p>}

      {showUsers ? (
        <div className="flex-grow overflow-y-auto h-[450px]">
          {usersToShown?.map((user) => (
            <AddFriendCard user={user} key={user.userId} />
          ))}
        </div>
      ) : (
        <div className="flex-grow overflow-y-auto h-[450px]">
          {AllUsersBySenderId?.map((chatInfo) => (
            <FriendChatCard
              chatInfo={chatInfo}
              key={chatInfo.receiverId}
            />
          ))}
        </div>
      )}
    </div>
  );
}

export default Connections;
