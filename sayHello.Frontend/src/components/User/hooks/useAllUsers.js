import { useMutation } from "@tanstack/react-query";
import { getAllUsers } from "../../../services/apiUser";

export function useAllUsers() {
  const {
    mutate,
    isLoading,
    error,
    data: AllUsers,
  } = useMutation({
    mutationKey: ["Users"],
    mutationFn: getAllUsers,
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { mutate, isLoading, error, AllUsers };
}
