import { useQuery } from "@tanstack/react-query";
import { getAllUsers } from "../../services/apiUser";

export function useAllUsers() {
  const {
    isLoading,
    error,
    data: AllUsers,
  } = useQuery({
    queryKey: ["Users"],
    queryFn: getAllUsers,
  });

  return { isLoading, error, AllUsers };
}
