import { useState } from "react";

export function useMessageBlocking() {
  const [preventedUsers, setPreventedUsers] = useState([]);

  const handlePreventSendMessage = (obj) => {
    setPreventedUsers((prev) => {
      const exists = prev.some(
        (user) => user.id === obj.id && user.name === obj.name
      );
      if (!exists) {
        return [...prev, obj];
      }
      return prev;
    });
  };

  const checkIfUserPrevented = (name, id) => {
    return preventedUsers.some((obj) => obj.name === name && obj.id === id);
  };

  const removePreventedUser = (name, id) => {
    setPreventedUsers((prev) =>
      prev.filter((obj) => obj.name !== name && obj.id !== id)
    );
  };

  console.log(preventedUsers);
  return {
    preventedUsers,
    handlePreventSendMessage,
    checkIfUserPrevented,
    removePreventedUser,
  };
}
