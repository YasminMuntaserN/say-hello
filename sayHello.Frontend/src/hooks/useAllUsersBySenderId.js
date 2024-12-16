import { useMutation, useQueryClient } from "@tanstack/react-query";
import { getAllBySenderId } from "../services/apiUser";

export function useAllUsersBySenderId() {
  const queryClient = useQueryClient();

  const {
    mutate,
    isLoading,
    error,
    data: AllUsers,
  } = useMutation({
    mutationFn: getAllBySenderId,
    onSuccess: () => {
      console.log("Messages get successfully");
      queryClient.invalidateQueries({ queryKey: ["Messages"] });
    },
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });
  return { mutate, isLoading, error, AllUsers };
}
