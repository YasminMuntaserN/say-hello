import { useMutation, useQueryClient } from "@tanstack/react-query";
import { addUser, DeleteUser } from "../../../services/apiUser";
import toast from "react-hot-toast";

export function useAddUser() {
  const queryClient = useQueryClient();

  const {
    mutate,
    status,
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
  return { mutate, isLoading: status === "pending", error, User };
}

export function useDeleteUser() {
  const queryClient = useQueryClient();

  const { mutate, status, error } = useMutation({
    mutationFn: DeleteUser,
    onSuccess: () => {
      toast.success("User Deleted successfully");
      queryClient.invalidateQueries({ queryKey: ["Users"] });
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
  });
  return { mutate, isLoading: status === "pending", error };
}
