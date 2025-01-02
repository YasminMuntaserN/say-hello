import { createContext, useContext } from "react";
import { useState } from "react";
import { getStoredUser ,setStoredUser,removeStoredUser } from "../utils/storage";

const ChatContext = createContext();
export function ChatProvider({ children }) {
  const [userInChat ,setInChat] = useState(null);
  const [showUsers ,setShowUsers] = useState(false);
  const [user, setUser] = useState(getStoredUser());
  const [refetchChats, setRefetchChats] = useState(false);
  const [showChatPartnerOperations, setShowChatPartnerOperations] = useState(false);
  const [updatedPartnerOperations, setUpdatedPartnerOperations] = useState(false);


  const login = (userInfo) => {
    setStoredUser(userInfo);
    setUser(userInfo);
    console.log(userInfo);
  };

  const logout = () => {
    removeStoredUser();
    setUser(null);
  };
  //here we want to but the chat partner
  function setUserInChat(value) {
    // here the chat will be from previous chats
    if (value.chatPartnerId && value.chatPartnerName && value.chatPartnerImage) {
      const mappedChatInfoFromPreviousChats = {
          userId: value.chatPartnerId,
          receiverName :value.chatPartnerName,
          receiverImage:value.chatPartnerImage,
          lastMessageStatus:value.lastMessageStatus
        };
      setInChat({type:mappedChatInfoFromPreviousChats ,from:"previous chat partner"});
    } 
    //here the chat will be from the add friends 
    else if (value.userId && value.username && value.profilePictureUrl && value.status) {
      const mappedChatInfo = {
        userId: value.userId,
        receiverImage: value.profilePictureUrl,
        receiverName: value.username,
        lastMessageStatus: value.status
      };

      setInChat({type:mappedChatInfo ,from:"new chat partner"});
    } 
    //here the chat will be from the add group 
    else if (value.userId && value.username && value.profilePictureUrl) {
    const mappedChatInfo = {
      userId: value.userId,
      receiverImage: value.profilePictureUrl,
      receiverName: value.username,
      lastMessageStatus: "Read"
    };
    setInChat({type:mappedChatInfo ,from:"group"});
  } else {
      console.log("Unknown object structure. Cannot set user in chat.");
    }
  }


  return (
    <ChatContext.Provider value={{ user, login, logout,showUsers ,setShowUsers ,
    userInChat ,setUserInChat ,refetchChats, setRefetchChats,
    showChatPartnerOperations, setShowChatPartnerOperations ,updatedPartnerOperations, setUpdatedPartnerOperations}}>
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
