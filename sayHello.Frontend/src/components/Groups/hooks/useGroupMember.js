import { useMutation } from "@tanstack/react-query";
import { addGroupMember } from "../../../services/apiGroup";

export function useAddGroupMember() {
  const {
    mutate,
    isLoading,
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
  return { mutate, isLoading, error, GroupMember };
}
