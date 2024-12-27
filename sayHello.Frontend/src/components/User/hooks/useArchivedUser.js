import { useMutation, useQuery } from "@tanstack/react-query";
import {
  addArchivedUser,
  DeleteArchivedUser,
  getAllBArchivedUsers,
  getAllBArchivedUsersCount,
  isArchivedUser,
} from "../../../services/apiUser";
import toast from "react-hot-toast";

export function useArchivedUser() {
  const {
    mutate: ArchivedUser,
    isLoading,
    error,
    data: User,
  } = useMutation({
    mutationFn: addArchivedUser,
    onSuccess: () => {
      toast.success("User Archived successfully");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: "Users",
  });
  return { ArchivedUser, isLoading, error, User };
}

export function useArchivedUserCount() {
  const {
    mutate: ArchivedUser,
    isLoading,
    error,
    data: ArchivedUsersCount,
  } = useMutation({
    mutationKey: ["ArchivedUsers"],
    mutationFn: getAllBArchivedUsersCount,
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { ArchivedUser, isLoading, error, ArchivedUsersCount };
}

export function useIsArchivedUser(ArchivedUserId, ArchivedByUserId) {
  const { error, isLoading, data } = useQuery({
    queryFn: () => isArchivedUser(ArchivedUserId, ArchivedByUserId),
    queryKey: ["ArchivedUsers", { ArchivedUserId, ArchivedByUserId }],
  });

  return { error, isLoading, isArchived: data };
}

export function useDeleteArchivedUser() {
  const {
    mutate: unArchivedUser,
    isLoading,
    error,
    data: IsUnArchived,
  } = useMutation({
    mutationFn: DeleteArchivedUser,
    onSuccess: () => {
      toast.success("User UnArchived successfully");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: ["ArchivedUsers"],
  });
  return { unArchivedUser, isLoading, error, IsUnArchived };
}

export function useAllArchivedUsers() {
  const {
    mutate,
    isLoading,
    error,
    data: AllArchivedUsers,
  } = useMutation({
    mutationKey: ["ArchivedUsers"],
    mutationFn: getAllBArchivedUsers,
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { mutate, isLoading, error, AllArchivedUsers };
}
