import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addUser } from "../../../services/apiUser";
import toast from "react-hot-toast";

export function useAddUser() {
  const queryClient = useQueryClient();

  const {
    mutate,
    isLoading,
    error,
    data: User,
  } = useMutation({
    mutationFn: addUser,
    onSuccess: () => {
      toast.success("User added successfully");
      queryClient.invalidateQueries({ queryKey: ["Users"] });
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
  });
  return { mutate, isLoading, error, User };
}
