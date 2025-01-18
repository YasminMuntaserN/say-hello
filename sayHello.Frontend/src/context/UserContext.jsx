import { createContext, useContext, useState } from 'react';
import { getStoredUser, setStoredUser, removeStoredUser } from '../utils/storage';
import { useMessageBlocking } from '../components/Messages/hooks/useMessageBlocking';

const ChatContext = createContext();

export function ChatProvider({ children }) {
  const [userInChat, setInChat] = useState(null);
  const [showUsers, setShowUsers] = useState(false);
  const [user, setUser] = useState(getStoredUser());
  const [refetchChats, setRefetchChats] = useState(false);
  const [showChatPartnerOperations, setShowChatPartnerOperations] = useState(false);
  const [updatedPartnerOperations, setUpdatedPartnerOperations] = useState("");
  const [usersToShow, setUsersToShow] = useState([]);

  const {
    preventedUsers,
    handlePreventSendMessage,
    checkIfUserPrevented,
    removePreventedUser
  } = useMessageBlocking();

  const login = (userInfo) => {
    setStoredUser(userInfo);
    setUser(userInfo);
  };

  const logout = () => {
    removeStoredUser();
    setUser(null);
  };

  function setUserInChat(value) {
    if (value.chatPartnerId && value.chatPartnerName && value.chatPartnerImage) {
      const mappedChatInfo = {
        userId: value.chatPartnerId,
        receiverName: value.chatPartnerName,
        receiverImage: value.chatPartnerImage,
        lastMessageStatus: value.lastMessageStatus,
        bio: value.bio
      };
      setInChat({ type: mappedChatInfo, from: "previous chat partner" });
    } 
    else if (value.userId && value.username && value.profilePictureUrl && value.status) {
      const mappedChatInfo = {
        userId: value.userId,
        receiverImage: value.profilePictureUrl,
        receiverName: value.username,
        lastMessageStatus: value.status,
        bio: value.bio
      };
      setInChat({ type: mappedChatInfo, from: "new chat partner" });
    }
    else if (value.userId && value.username && value.profilePictureUrl) {
      const mappedChatInfo = {
        userId: value.userId,
        receiverImage: value.profilePictureUrl,
        receiverName: value.username,
        lastMessageStatus: "Read",
        bio: ""
      };
      setInChat({ type: mappedChatInfo, from: "group" });
    } 
    else if (value.groupId && value.username && value.userId&& value.userImg) {
      const mappedChatInfo = {
        userId: value.userId,
        receiverImage: value.userImg,
        receiverName: value.username,
        lastMessageStatus: "Read",
        bio: value.bio
      };
      setInChat({ type: mappedChatInfo, from: "group member" });
    }else {
      setInChat(null);
    }
  }

  return (
    <ChatContext.Provider value={{
      user,
      login,
      logout,
      showUsers,
      setShowUsers,
      userInChat,
      setUserInChat,
      refetchChats,
      setRefetchChats,
      usersToShow,
      setUsersToShow,
      showChatPartnerOperations,
      setShowChatPartnerOperations,
      updatedPartnerOperations,
      setUpdatedPartnerOperations,
      preventedUsers,
      handlePreventSendMessage,
      checkIfUserPrevented,
      removePreventedUser
    }}>
      {children}
    </ChatContext.Provider>
  );
}

export function useChat() {
  const context = useContext(ChatContext);
  if (!context) {
    throw new Error('useChat must be used within a ChatProvider');
  }
  return context;
}
