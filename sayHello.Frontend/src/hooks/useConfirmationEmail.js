import { useMutation, useQueryClient } from "@tanstack/react-query";
import { handleConfirmationEmail } from "../services/apiUser";
import toast from "react-hot-toast";

export function useConfirmationEmail() {
  const queryClient = useQueryClient();
  const { mutate, isLoading } = useMutation({
    mutationFn: handleConfirmationEmail,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["Email"] });
      toast.success(
        "Please check your inbox for the newly sent verification email."
      );
    },
    onError: (err) => {
      console.log(`${err.message}`);
    },
  });

  return { isLoading, mutate };
}
