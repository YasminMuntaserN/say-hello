import { FaUserLock } from "react-icons/fa6";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";

import Button from "../ui/Button"
import FormRow from "../ui/FormRow"
import FormContainer from "../ui/FormContainer";
import {useAddUser} from "../hooks/useAddUser";
import SpinnerMini from "../ui/SpinnerMini";
import { useConfirmationEmail } from "../hooks/useConfirmationEmail";
import { useUser } from "../context/UserContext";


function SignupForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm();
  const navigate =useNavigate();
  const { mutate, User, isLoading } = useAddUser();
  const { mutate :conformingEmail } = useConfirmationEmail();
  const { login }=useUser();

  function onSubmit(data) {
    console.log(data);

    if (data) {
      const formData = new FormData();
      formData.append("Username", data.Name);
      formData.append("Email", data.Email);
      formData.append("Password", data.Password);
      formData.append("Bio", data.Bio?data.Bio:"");
      formData.append("Status", "Online");
      formData.append("ProfilePictureUrl", "test");
      if (data.ProfilePicture && data.ProfilePicture.length > 0) {
        formData.append("photo", data.ProfilePicture[0]);
      } else {
        console.error("No photo selected");
        formData.append("photo","" );
      }
      console.log(formData);
      mutate(formData, {
        onSuccess: (data) => {
          console.log(`{SignupForm ${data}`);
          reset(); 
          navigate('/verify-email');
          conformingEmail(data.email);
          login(data);
        }
      });
    }
  }

  console.log(isLoading);
  console.log(User);

  return (
    <FormContainer header ="Sign Up to Start Chatting!">
      <form onSubmit={handleSubmit(onSubmit)}>
        <FormRow type="text" errors={errors} register={register} FieldName="Name" />
        <FormRow type="text" errors={errors} register={register} FieldName="Bio" requiredField={false} />
        <FormRow type="email" errors={errors} register={register} FieldName="Email" />
        <FormRow type="password" errors={errors} register={register} FieldName="Password" />
        
        <div className="flex mb-5 align-middle">
        <label className="text-lightText mr-4" htmlFor="ProfilePictureUrl" >ProfilePicture </label>
        <input type="file" id="ProfilePicture"  accept="image/*" {...register("ProfilePicture")} />
        </div>
        
        <Button type="submit">
          {isLoading ?  <SpinnerMini/> :<> signup... <FaUserLock /></>}
        </Button>
      </form>
      </FormContainer>
  )
}
export default SignupForm
