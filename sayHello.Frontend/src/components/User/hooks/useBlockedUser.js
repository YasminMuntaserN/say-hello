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
    status,
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
  return { BlockedUser, isLoading: status === "pending", error, User };
}

export function useBlockedUserCount() {
  const queryClient = useQueryClient();
  const {
    mutate: BlockedUser,
    status,
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

  return {
    BlockedUser,
    isLoading: status === "pending",
    error,
    BlockedUsersCount,
  };
}

export function useIsBlockedUser(BlockedUserId, BlockedByUserId) {
  const { error, status, data } = useQuery({
    queryFn: () => isBlockedUser(BlockedUserId, BlockedByUserId),
    queryKey: ["BlockedUsers", { BlockedUserId, BlockedByUserId }],
  });

  return { error, isLoading: status === "pending", isBlocked: data };
}

export function useDeleteBlockedUser() {
  const queryClient = useQueryClient();
  const {
    mutate: unBlockedUser,
    status,
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
  return { unBlockedUser, isLoading: status === "pending", error, IsUnBlocked };
}

export function useAllBlockedUsers() {
  const queryClient = useQueryClient();
  const {
    mutate,
    status,
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

  return { mutate, isLoading: status === "pending", error, AllBlockedUsers };
}
