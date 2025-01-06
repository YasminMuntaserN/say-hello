import { useMutation } from "@tanstack/react-query";
import {
  addGroupMember,
  deleteGroupMember,
  getAllGroupMembers,
} from "../../../services/apiGroup";

export function useAddGroupMember() {
  const {
    mutate,
    status,
    error,
    data: GroupMember,
  } = useMutation({
    mutationFn: addGroupMember,
    onSuccess: () => {
      console.log("Group added successfully");
    },
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });
  return { mutate, isLoading: status === "pending", error, GroupMember };
}

export function useAllGroupMembers() {
  const {
    mutate,
    status,
    error,
    data: AllGroupMembers,
  } = useMutation({
    mutationKey: ["GroupMember"],
    mutationFn: getAllGroupMembers,
    onSuccess: (data) => {
      console.log("Groups retrieved successfully", data);
    },
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });
  return { mutate, isLoading: status === "pending", error, AllGroupMembers };
}

export function useLeaveGroup() {
  const { mutate, status, error } = useMutation({
    mutationKey: ["GroupMember"],
    mutationFn: deleteGroupMember,
    onSuccess: (data) => {
      console.log("Group Member deleted successfully", data);
    },
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });
  return { mutate, isLoading: status === "pending", error };
}
