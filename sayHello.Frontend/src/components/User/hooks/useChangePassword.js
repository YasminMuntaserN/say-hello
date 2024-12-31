import { useMutation } from "@tanstack/react-query";
import {
  ChangePassword,
  handleRestorePassword,
} from "../../../services/apiUser";
import toast from "react-hot-toast";

export function useChangePassword() {
  const { mutate, error, loading } = useMutation({
    mutationFn: (data) => ChangePassword(data.id, data.newPassword),
    onSuccess: () => {
      toast.success("User Password has been updated");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: "Users",
  });
  return { mutate, error, loading };
}

export function useRestorePassword() {
  const { mutate, error, loading } = useMutation({
    mutationFn: (data) => handleRestorePassword(data.email),
    onSuccess: () => {
      toast.success("Please check your inbox and follow the link provided.");
    },
    onError: (err) => {
      toast.error(`${err.message}`);
    },
    mutationKey: "Users",
  });
  return { mutate, error, loading };
}
