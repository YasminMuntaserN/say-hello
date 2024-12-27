import { useMutation, useQuery } from "@tanstack/react-query";
import {
  addBlockedUser,
  DeleteBlockedUser,
  getAllBlockedUsers,
  getAllBlockedUsersCount,
  isBlockedUser,
} from "../../../services/apiUser";
import toast from "react-hot-toast";

export function useBlockedUser() {
  const {
    mutate: BlockedUser,
    isLoading,
    error,
    data: User,
  } = useMutation({
    mutationFn: addBlockedUser,
    onSuccess: () => {
      toast.success("User Blocked successfully");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: "Users",
  });
  return { BlockedUser, isLoading, error, User };
}

export function useBlockedUserCount() {
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
  const {
    mutate: unBlockedUser,
    isLoading,
    error,
    data: IsUnBlocked,
  } = useMutation({
    mutationFn: DeleteBlockedUser,
    onSuccess: () => {
      toast.success("User UnBlocked successfully");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: ["BlockedUsers"],
  });
  return { unBlockedUser, isLoading, error, IsUnBlocked };
}

export function useAllBlockedUsers() {
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
  });

  return { mutate, isLoading, error, AllBlockedUsers };
}
