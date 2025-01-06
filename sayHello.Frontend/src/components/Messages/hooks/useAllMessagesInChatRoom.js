import { useMutation } from "@tanstack/react-query";
import {
  getMessagesInChatRoom,
  GetMessagesInChatRoomForGroup,
} from "../../../services/apiMessage";

export function useAllMessagesInChatRoom(from) {
  const {
    mutate,
    status,
    error,
    data: AllMessages,
  } = useMutation({
    mutationKey: ["Messages"],
    mutationFn:
      from === "group" ? GetMessagesInChatRoomForGroup : getMessagesInChatRoom,
    onError: (err) => {
      console.error(`Error fetching ${from} messages:`, err.message);
    },
  });

  return { mutate, isLoading: status === "pending", error, AllMessages };
}

// export function useAllMessagesInChatRoomForGroup() {
//   const {
//     mutate,
//     status,
//     error,
//     data: AllMessages,
//   } = useMutation({
//     mutationKey: ["GroupMessages"],
//     mutationFn: GetMessagesInChatRoomForGroup,
//     onSuccess: () => console.log(`all group messages received`),
//     onError: (err) => {
//       console.log(`${err.message}`);
//     },
//   });

//   return { mutate,  isLoading: status === "pending", error, AllMessages };
// }
