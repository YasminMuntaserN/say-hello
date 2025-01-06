import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";
import {
  addGroup,
  getAllGroups,
  getGroupsCount,
} from "../../../services/apiGroup";

export function useAddGroup() {
  const {
    mutate,
    status,
    error,
    data: Group,
  } = useMutation({
    mutationKey: ["Groups"],
    mutationFn: addGroup,
    onSuccess: () => {
      toast.success("Group added successfully");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
  });
  return { mutate, isLoading: status === "pending", error, Group };
}

export function useAllGroups() {
  const {
    mutate,
    status,
    error,
    data: AllGroups,
  } = useMutation({
    mutationKey: ["Groups"],
    mutationFn: getAllGroups,
    onSuccess: (data) => {
      console.log("Groups retrieved successfully", data);
    },
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });
  return { mutate, isLoading: status === "pending", error, AllGroups };
}

export function useGroupsCount() {
  const {
    mutate: Group,
    status,
    error,
    data: GroupsCount,
  } = useMutation({
    mutationKey: ["Groups"],
    mutationFn: getGroupsCount,
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { Group, isLoading: status === "pending", error, GroupsCount };
}
