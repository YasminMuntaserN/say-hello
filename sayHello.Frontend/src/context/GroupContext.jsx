import { createContext, useContext, useState } from "react";
import { useAddGroupMember } from "../components/Groups/hooks/useGroupMember";
import { useChat } from "./UserContext";

const GroupContext = createContext();

export function GroupProvider({ children }) {
  const {user}=useChat();
  const [MemberInGroup, setMemberInGroup] = useState([user?.userId]);
  const [updateGroupMembers, setUpdateGroupMembers] = useState(false);
  const { mutate } = useAddGroupMember();

  const addGroupMember = (userId) => {
    setMemberInGroup((prev) => 
      prev.includes(userId) ? prev : [...prev, userId]
    );
  };

  const RemoveGroupMember = (userId) => {
    setMemberInGroup((prev) => prev.filter((memberId) => memberId !== userId));
  };

  const SaveGroupMembers = async (groupId) => {
    try {
      await Promise.all(
        MemberInGroup.map((userId) =>
          mutate({ groupId, userId }, {
            onSuccess: (data) => {
              console.log("groupMember", data);
            },
            onError: (error) => {
              console.error("Error adding group member:", error);
            },
          })
        )
      );
    } catch (error) {
      console.error("Error saving group members:", error);
    }
  };

  return (
    <GroupContext.Provider
      value={{
        MemberInGroup,
        setMemberInGroup,
        addGroupMember,
        RemoveGroupMember,
        SaveGroupMembers,
        updateGroupMembers, 
        setUpdateGroupMembers
      }}
    >
      {children}
    </GroupContext.Provider>
  );
}

export function useGroup() {
  const context = useContext(GroupContext);
  if (!context) {
    throw new Error("useGroup must be used within a GroupProvider");
  }
  return context;
}
