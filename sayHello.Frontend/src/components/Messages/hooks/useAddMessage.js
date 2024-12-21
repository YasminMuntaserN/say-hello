import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { addMessage } from "../../../services/apiMessage";

export function useAddMessage() {
  const {
    mutate,
    isLoading,
    error,
    data: message,
  } = useMutation({
    mutationKey: "Messages",
    mutationFn: addMessage,
    onSuccess: () => {
      toast.success("User added successfully");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
  });
  return { mutate, isLoading, error, message };
}
