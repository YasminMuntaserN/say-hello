import { FaUserLock } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";

import Button from "../ui/Button"
import FormRow from "../ui/FormRow"
import FormContainer from "../ui/FormContainer";
import {useAddUser} from  "../components/User/hooks/useAddUser";
import SpinnerMini from "../ui/SpinnerMini";
import { useChat } from "../context/UserContext";
import { useAuth } from "./User/hooks/useAuth";
import PasswordFormRow from "../ui/PasswordFormRow";


function SignupForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm();
  const navigate =useNavigate();
  const { mutate, isLoading :isAdding } = useAddUser();
  const {loginMutate,isLoading } =useAuth();
  const { login }=useChat();

  function onSubmit(data) {
    if (data) {
      const formData = new FormData();
      formData.append("Username", data.Name);
      formData.append("Email", data.Email);
      formData.append("Password", data.Password);
      formData.append("Bio", data.Bio?data.Bio:"");
      formData.append("Status", "Online");
      formData.append("ProfilePictureUrl", "test");
      formData.append("Role", "User");
      if (data.ProfilePicture && data.ProfilePicture.length > 0) {
        formData.append("photo", data.ProfilePicture[0]);
      } else {
        console.error("No photo selected");
        formData.append("photo","" );
      }
      mutate(formData, {
        onSuccess: (data) => {
          console.log(data);
          reset(); 
          navigate('/verify-email');
          loginMutate({email:data.user.email,password: data.user.password},
            {
              onSuccess: (data) => {
                if(data){
                reset();
                navigate(`/dashboard/${data.user.username}`);
                login(data.user);
                }
              },
              onError: (error) => {
                console.error("Submission failed:", error);
                reset(); 
              },
            });
        }
      });
    }
  }

  return (
    <FormContainer header ="Sign Up to Start Chatting!">
      <form onSubmit={handleSubmit(onSubmit)}>
        <FormRow type="text" errors={errors} register={register} FieldName="Name" />
        <FormRow type="text" errors={errors} register={register} FieldName="Bio" requiredField={false} />
        <FormRow type="email" errors={errors} register={register} FieldName="Email" />
        <PasswordFormRow  errors={errors} register={register} />
        <div className="flex mb-5 align-middle">
        <label className="text-lightText mr-4" htmlFor="ProfilePictureUrl" >ProfilePicture </label>
        <input type="file" id="ProfilePicture"  accept="image/*" {...register("ProfilePicture")} />
        </div>
        
        <Button variant="submit" type="submit">
          {isLoading || isAdding ?  <SpinnerMini/> :<> signup... <FaUserLock /></>}
        </Button>
      </form>
      </FormContainer>
  )
}
export default SignupForm
