import { useNavigate } from "react-router-dom";
import { useChat } from "../../context/UserContext";
import Button from "../../ui/Button"
import { useDeleteUser } from "./hooks/useAddUser"
import SpinnerMini from "../../ui/SpinnerMini";
function DeleteForm({onClose}) {
  const navigation =useNavigate();
  const {user}=useChat();
  const { mutate, isLoading, error }=useDeleteUser();

  const handleDeleteUser =() => {
    mutate(user.userId,{
      onSuccess: () => {
        onClose?.();
        navigation("/login");
      }
  });
}
  if(error){
    return <p className="text-xl font-bold text-red-600">there is something get wrong {error.message}</p>
  }
  return (
      <div className="w-[400px]  p-5 ">
      <div className="border-red-600 border-2 p-10 rounded-xl text-center">
            <h2 className="text-xl font-bold text-red-600">
              Are you sure you want to delete your account?
            </h2>
            <p className="m-4 ">
              All your chats and account data will be permanently deleted. This action cannot be undone.
            </p>
            <div>
                <Button  variant="reset" onClick={onClose}> Cancel </Button>
                <Button variant="save" type="submit" onClick={()=>handleDeleteUser()}>
                {isLoading ? <SpinnerMini /> : "Save Changes"}
                </Button>
            </div>
        </div>
      </div>
  )
}

export default DeleteForm
