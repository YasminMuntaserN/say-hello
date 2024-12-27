import { useMutation } from "@tanstack/react-query";
import { getAllBySenderId } from "../../../services/apiUser";

export function useAllUsersBySenderId() {
  const {
    mutate,
    isLoading,
    error,
    data: AllUsers,
  } = useMutation({
    mutationKey: ["Messages"],
    mutationFn: getAllBySenderId,
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { mutate, isLoading, error, AllUsers };
}
