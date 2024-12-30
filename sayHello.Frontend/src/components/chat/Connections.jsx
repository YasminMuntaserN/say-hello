import { useEffect, useState } from 'react';
import { useAllUsersBySenderId } from '../user/hooks/useAllUsersBySenderId';
import  SearchBar  from '../../ui/SearchBar';
import  QuickActions  from './QuickActions';
import  FriendChatCard  from './FriendChatCard';
import  AddFriend  from './AddFriend';
import  AddFriendCard  from './AddFriendCard';
import { useChat } from '../../context/UserContext';

export function Connections() {
  const { user, showUsers, refetchChats, updatedPartnerOperations } = useChat();
  const { 
    mutate, 
    isLoading, 
    error, 
    AllUsers: AllUsersBySenderId 
  } = useAllUsersBySenderId();
  
  const [usersToShow, setUsersToShow] = useState([]);

  useEffect(() => {
    if (user?.userId) {
      mutate(user.userId);
    }
  }, [mutate, user?.userId, refetchChats, updatedPartnerOperations]);
  
  return (
    <div className="bg-[#f8fafc] h-screen flex flex-col">
      <QuickActions setUsersToShow={setUsersToShow} AllUsersBySenderId={AllUsersBySenderId}/>
      <AddFriend setUsersToShow={setUsersToShow} />
      <SearchBar />
      
      {isLoading && <p>Loading messages...</p>}
      {error && <p>Error fetching messages: {error.message}</p>}

      <div className="flex-grow overflow-y-auto h-[450px]">
        {showUsers ? (
          usersToShow?.map((user) => (
            <AddFriendCard key={user.userId} user={user} />
          ))
        ) : (
          AllUsersBySenderId?.map((chatInfo) => (
            <FriendChatCard
              key={chatInfo.receiverId}
              chatInfo={chatInfo}
            />
          ))
        )}
      </div>
    </div>
  );
}

export default Connections;
