import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-hot-toast";
import { login, logout } from "../../../services/authService";

export function useAuth() {
  const queryClient = useQueryClient();

  const {
    mutate: loginMutate,
    status,
    error,
    data: user,
  } = useMutation({
    mutationFn: ({ email, password }) => login(email, password),
    onSuccess: (data) => {
      toast.success(`Welcome ${data.user.username}`);
      queryClient.invalidateQueries({ queryKey: ["Users"] });
    },
    onError: (err) => {
      console.error("Login error:", err);
      toast.error("Invalid credentials");
    },
  });

  return { loginMutate, isLoading: status === "pending", error, user };
}

export function useLogout() {
  const {
    mutate: logoutMutate,
    status,
    error,
  } = useMutation({
    mutationFn: () => logout(),
    onError: (err) => {
      console.error("logout error:", err);
    },
  });

  return { logoutMutate, isLoading: status === "pending", error };
}
