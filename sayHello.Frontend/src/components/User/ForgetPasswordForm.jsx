import { useForm } from "react-hook-form";
import FormRow from "../../ui/FormRow"
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";
import { useChangePassword } from "./hooks/useChangePassword";
import { useChat } from "../../context/UserContext";
import PasswordFormRow from "../../ui/PasswordFormRow";

function ForgetPasswordForm({onClose}) {
  const {user} =useChat();
  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
  } = useForm();
    const{mutate:changePassword ,isLoading}=useChangePassword();
    const onSubmit =(data)=> {
      changePassword({ id: user.userId, newPassword: data.NewPassword },{
        onSuccess: () => {
          onClose?.();
        }
    });
    }
  return (
    <div className="w-[700px]  p-5 ">
      <div className="border-[#6b4ca0] border-2 p-5 rounded-xl ">
      <img src="/password.png" alt="password" className="w-[300px] h-[300px] mt-[-160px] ml-[150px]"/>
      <h1 className="text-center text-secondary  mt-[-70px] mb-10 font-bold text-3xl">Change Password</h1>
      
      <form onSubmit={handleSubmit(onSubmit)}>
      <PasswordFormRow 
        errors={errors}
        register={register}
        label="New Password"
      />
      <FormRow
        type="password"
        errors={errors}
        register={register}
        FieldName="ConfirmPassword"
        rules={{
          required: "Confirm password is required",
          validate: (value) =>
            value === watch("Password") || "Passwords do not match",
        }}
        label="Confirm Password"
      />
      <div className="ml-[60%]">
      <Button variant="reset" onClick={onClose}>Cancel</Button>
      <Button variant="save" type="submit">
          {isLoading ? <SpinnerMini /> : "Save Changes"}
      </Button>
      </div>
      </form>
      </div>
    </div>
  )
}

export default ForgetPasswordForm
