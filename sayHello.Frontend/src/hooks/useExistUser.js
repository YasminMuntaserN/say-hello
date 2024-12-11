import { useMutation, useQueryClient } from "@tanstack/react-query";
import { handleCheckUserByEmailAndPassword } from "../services/apiUser";
import { toast } from "react-hot-toast";

export function useExistUser() {
  const queryClient = useQueryClient();

  const {
    mutate,
    isLoading,
    error,
    data: User,
  } = useMutation({
    mutationFn: ({ Email, Password }) =>
      handleCheckUserByEmailAndPassword(Email, Password),
    onSuccess: (User) => {
      if (User) {
        toast.success(`Welcome ${User.username}`);
        queryClient.invalidateQueries({ queryKey: ["Users"] });
      } else {
        console.log("No user found with the provided credentials.");
        toast.error(`user found 🥲`);
      }
    },
    onError: (err) => {
      console.error("An error occurred:", err);
      toast.error(`user found 🥲`);
    },
  });

  return { mutate, isLoading, error, User };
}
