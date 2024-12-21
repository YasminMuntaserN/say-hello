import { useMutation } from "@tanstack/react-query";
import { getMessagesInChatRoom } from "../../../services/apiMessage";

export function useAllMessagesInChatRoom() {
  const {
    mutate,
    isLoading,
    error,
    data: AllMessages,
  } = useMutation({
    mutationKey: ["Messages"],
    mutationFn: getMessagesInChatRoom,
    onSuccess: () => console.log(`all messages received`),
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { mutate, isLoading, error, AllMessages };
}
