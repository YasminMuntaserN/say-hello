import { useMutation } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { addGroup, getAllGroups } from "../../../services/apiGroup";

export function useAddGroup() {
  const {
    mutate,
    isLoading,
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
  return { mutate, isLoading, error, Group };
}

export function useAllGroups() {
  const {
    mutate,
    isLoading,
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
  return { mutate, isLoading, error, AllGroups };
}
