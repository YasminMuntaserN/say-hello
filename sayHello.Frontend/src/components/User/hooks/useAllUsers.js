import { useMutation } from "@tanstack/react-query";
import { getAllUsers } from "../../../services/apiUser";

export function useAllUsers() {
  const {
    mutate,
    status,
    error,
    data: AllUsers,
  } = useMutation({
    mutationKey: ["Users"],
    mutationFn: getAllUsers,
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { mutate, isLoading: status === "pending", error, AllUsers };
}
