import { useForm } from "react-hook-form";
import FormRow from "../../ui/FormRow"
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";
import { FaCircleInfo } from "react-icons/fa6";
import { useRestorePassword } from "./hooks/useChangePassword";


function CheckEmail({onClose}) {
    const {register,formState: { errors },handleSubmit } = useForm();
    const{mutate:changePassword ,loading}=useRestorePassword();
    const {login}=useChat();
    const onSubmit =(formData)=> {
      changePassword(formData.Email,{
        onSuccess: (data) => {
          login(data.use);
          onClose?.();
        }
    });
        }
  return (
    <div className="w-[700px]  p-5 ">
    <div className="border-[#6b4ca0] border-2 p-10 rounded-xl ">
    <img src="/password.png" alt="password" className="w-[300px] h-[300px] mt-[-175px] ml-[150px]"/>
    <h1 className="text-center text-secondary  mt-[-70px] mb-10 font-bold text-3xl">Reset Password</h1>
    <form onSubmit={handleSubmit(onSubmit)}>
    <FormRow 
          type="email"
          errors={errors}
          register={register}
          FieldName="Email"
        />
    <p className="flex text-center text-lightText mt-5 gap-5">
      <FaCircleInfo className="text-green-600 mt-1 text-xl" />
      after submit We will sent you an email. Please check your inbox and follow the link provided.
    </p>
      <div className="ml-[60%] mt-5">
      <Button variant="reset" onClick={onClose}>Cancel</Button>
      <Button variant="save" type="submit">{loading ? <SpinnerMini /> : "Submit"}</Button>
      </div>
      </form>
    </div>
    
  </div>

  )
}

export default CheckEmail
