import { useEffect } from "react";
import { useUser } from "../../context/UserContext";
import { useAllUsersBySenderId } from "../../hooks/Users/useAllUsersBySenderId.js";
import { useAllUsers } from "../../hooks/Users/useAllUsers";
import SearchBar from "../../ui/SearchBar"
import QuickActions from "./QuickActions"
import FriendChatCard from "./FriendChatCard";
import AddFriend from "./AddFriend";
import AddFriendCard from "./AddFriendCard";

function Connections() {
    const { user ,showUsers} = useUser ();
    const { mutate, isLoading, error, AllUsers :AllUsersBySenderId } = useAllUsersBySenderId();
    const {isLoading :LoadingAllUsers, error :ErrorAllUser, AllUsers }=useAllUsers();
 
    useEffect(() => {
      if (user) { 
        mutate(user.userId); 
      }
    }, [mutate, user]);
  
    return ( 
      <div className="bg-[#f8fafc] h-screen flex flex-col">
        <QuickActions />
        <AddFriend />
        <SearchBar />
        {isLoading && <p>Loading messages...</p>}
        {error && <p>Error fetching messages: {error.message}</p>}
        <div className="flex-grow overflow-y-auto h-[400px]">
          {showUsers
            ? AllUsers?.map((user) => (
                <AddFriendCard user={user} key={user.userId} />
              ))
            : AllUsersBySenderId?.map((chatInfo) => (
                <FriendChatCard
                  chatInfo={chatInfo}
                  userId={user.userId}
                  key={chatInfo.receiverId}
                />
              ))}
        </div>
      </div>
    );
}

export default Connections
