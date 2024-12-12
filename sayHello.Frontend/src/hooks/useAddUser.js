import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addUser } from "../services/apiUser";
import toast from "react-hot-toast";

export function useAddUser() {
  const queryClient = useQueryClient();
  const {
    mutate,
    isLoading,
    error,
    data: User,
  } = useMutation({
    mutationFn: (userEntity) => addUser(userEntity),

    onSuccess: (User) => {
      if (User) {
        toast.success(`User added successfully`);
        queryClient.invalidateQueries({ queryKey: ["Users"] });
      } else {
        toast.error(`the user not added.🥲`);
      }
    },
    onError: (err) => {
      toast.error(`the user not added. 🥲 `, err);
    },
  });
  return { mutate, isLoading, error, User };
}

export default useAddUser;
