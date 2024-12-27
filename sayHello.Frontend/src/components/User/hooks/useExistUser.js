import { useMutation, useQueryClient } from "@tanstack/react-query";
import { handleCheckUserByEmailAndPassword } from "../../../services/apiUser";
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
        toast.error(`There is no user found for the given email and password.`);
      }
    },
    onError: (err) => {
      console.error("An error occurred:", err);
      toast.error(`user not found 🥲`);
    },
  });

  return { mutate, isLoading, error, User };
}
