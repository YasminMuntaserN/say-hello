import { useEffect, useState } from 'react';
import { useAllUsersBySenderId } from '../user/hooks/useAllUsersBySenderId';
import SearchBar from '../../ui/SearchBar';
import QuickActions from './QuickActions';
import FriendChatCard from './FriendChatCard';
import AddFriend from './AddFriend';
import AddFriendCard from './AddFriendCard';
import { useChat } from '../../context/UserContext';
import LoadingChattingCards from '../../ui/LoadingChatingCards';

export function Connections() {
  const { user, showUsers, refetchChats, updatedPartnerOperations, usersToShow } = useChat();
  const { mutate, isLoading, error, AllUsers: AllUsersBySenderId } = useAllUsersBySenderId();
  
  const [filteredUsers, setFilteredUsers] = useState(AllUsersBySenderId); 

  const handleSearch = (query) => {
    if (!query.trim()) {
      setFilteredUsers(showUsers ? usersToShow : AllUsersBySenderId);
    } else {
      const filtered = showUsers ?
        usersToShow ?.filter((user) =>
        user.username.toLowerCase().includes(query.toLowerCase()))
      : 
      AllUsersBySenderId ?.filter((user) =>
        user.chatPartnerName.toLowerCase().includes(query.toLowerCase()) )
      ;
      setFilteredUsers(filtered);
    }
  };

  useEffect(() => {
    if (user?.userId) {
      mutate(user.userId);
    }
  }, [mutate, user?.userId, refetchChats, updatedPartnerOperations]);

  useEffect(() => {
    setFilteredUsers(showUsers ? usersToShow : AllUsersBySenderId);
  }, [showUsers, usersToShow, AllUsersBySenderId]);

  return (
    <div className="bg-[#f8fafc] flex flex-col">
      <QuickActions AllUsersBySenderId={AllUsersBySenderId} />
      <AddFriend />
      <SearchBar onSearch={handleSearch} />

      {isLoading && <LoadingChattingCards/>}
      {error && <p>Error fetching messages: {error.message}</p>}

      <div className="flex-grow overflow-y-auto h-[450px] relative mb-10">
        {filteredUsers?.map((user) =>
          showUsers ? (
            <AddFriendCard key={user.userId} user={user} />
          ) : (
            <FriendChatCard key={user.receiverId} chatInfo={user} />
          )
        )}
      </div>
    </div>
  );
}

export default Connections;
