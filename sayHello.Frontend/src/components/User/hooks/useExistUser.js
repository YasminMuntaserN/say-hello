import { useMutation } from "@tanstack/react-query";
import { handleCheckUserByEmailAndPassword } from "../../../services/apiUser";
import { toast } from "react-hot-toast";

export function useExistUserByEmail() {
  const {
    mutate: ExistUserByEmil,
    status,
    error,
    data: User,
  } = useMutation({
    mutationFn: (Email) => handleCheckUserByEmailAndPassword(Email),
    onError: (err) => {
      console.error("An error occurred:", err);
      toast.error(`user not found 🥲`);
    },
  });

  return { ExistUserByEmil, isLoading: status === "pending", error, User };
}
