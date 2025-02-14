import { Link, useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import { IoMdLogIn } from "react-icons/io";
import Button from "../ui/Button";
import FormRow from "../ui/FormRow";
import { useAuth } from "../components/User/hooks/useAuth";
import SpinnerMini from "../ui/SpinnerMini";
import FormContainer from "../ui/FormContainer";
import { useChat } from "../context/UserContext";
import ForgatPassword from "./User/ForgatPassword";

function LoginForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset
  } = useForm();

  const {loginMutate,isLoading } =useAuth();
  const navigate = useNavigate(); 
  const {login}=useChat();

  function onSubmit(data) {
    const {Email ,Password} =data;
    loginMutate({email:Email,password: Password},
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
  };

  return (
    <FormContainer header ="Welcome Back...">
      <span className={StyledP}>Please enter your email and password.</span>
      <form onSubmit={handleSubmit(onSubmit)}>
        <FormRow 
            type="email"
            errors={errors}
            register={register}
            FieldName="Email"
        />
        <FormRow 
              type="password"
              errors={errors}
              register={register}
              FieldName="Password"
        />
        <Button variant="submit" type="submit">
          {isLoading ?  <SpinnerMini/> : <>Login... <IoMdLogIn className="text-white text-3xl"  /></>}
          </Button>
      </form>
      <div className="flex ml-20  mt-[-50px] justify-end">
      <ForgatPassword />
      </div>
      <div className={StyledLine}></div>
        <p className={StyledNewAccount}>Don't Have an Account yet ?  <Link className={StyledLink} to="/signup">Create Account</Link></p>
    </FormContainer>
  );
}

const StyledP = "text-gray-600 font-normal";
const StyledLine = "bg-lightText w-full h-0.5 mt-5 ";
const StyledNewAccount = "text-lg mt-10 text-center text-lightText";
const StyledLink = "text-secondary font-bold underline";

export default LoginForm;
