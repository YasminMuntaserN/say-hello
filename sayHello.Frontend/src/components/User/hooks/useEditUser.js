import { useMutation } from "@tanstack/react-query";
import { editUser } from "../../../services/apiUser";
import toast from "react-hot-toast";

export function useEditUser() {
  const { mutate, error, status } = useMutation({
    mutationFn: (data) => editUser(data.user, data.id),
    onSuccess: () => {
      toast.success("User Edited successfully");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: "Users",
  });
  return { mutate, error, isLoading: status === "pending" };
}
