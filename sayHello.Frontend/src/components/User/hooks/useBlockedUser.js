import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  addBlockedUser,
  DeleteBlockedUser,
  getAllBlockedUsers,
  getAllBlockedUsersCount,
  isBlockedUser,
} from "../../../services/apiUser";
import toast from "react-hot-toast";

export function useBlockedUser() {
  const queryClient = useQueryClient();
  const {
    mutate: BlockedUser,
    isLoading,
    error,
    data: User,
  } = useMutation({
    mutationFn: addBlockedUser,
    onSuccess: () => {
      toast.success("User Blocked successfully");
      queryClient.invalidateQueries(["BlockedUsers"]);
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: ["BlockedUsers"],
  });
  return { BlockedUser, isLoading, error, User };
}

export function useBlockedUserCount() {
  const queryClient = useQueryClient();
  const {
    mutate: BlockedUser,
    isLoading,
    error,
    data: BlockedUsersCount,
  } = useMutation({
    mutationKey: ["BlockedUsers"],
    mutationFn: getAllBlockedUsersCount,
    onError: (err) => {
      console.log(`${err.message}`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries(["BlockedUsers"]);
    },
  });

  return { BlockedUser, isLoading, error, BlockedUsersCount };
}

export function useIsBlockedUser(BlockedUserId, BlockedByUserId) {
  const { error, isLoading, data } = useQuery({
    queryFn: () => isBlockedUser(BlockedUserId, BlockedByUserId),
    queryKey: ["BlockedUsers", { BlockedUserId, BlockedByUserId }],
  });

  return { error, isLoading, isBlocked: data };
}

export function useDeleteBlockedUser() {
  const queryClient = useQueryClient();
  const {
    mutate: unBlockedUser,
    isLoading,
    error,
    data: IsUnBlocked,
  } = useMutation({
    mutationFn: DeleteBlockedUser,
    onSuccess: () => {
      toast.success("User UnBlocked successfully");
      queryClient.invalidateQueries(["BlockedUsers"]);
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: ["BlockedUsers"],
  });
  return { unBlockedUser, isLoading, error, IsUnBlocked };
}

export function useAllBlockedUsers() {
  const queryClient = useQueryClient();
  const {
    mutate,
    isLoading,
    error,
    data: AllBlockedUsers,
  } = useMutation({
    mutationKey: ["BlockedUsers"],
    mutationFn: getAllBlockedUsers,
    onError: (err) => {
      console.log(`${err.message}`);
    },
    onSuccess: () => {
      queryClient.invalidateQueries(["BlockedUsers"]);
    },
  });

  return { mutate, isLoading, error, AllBlockedUsers };
}
