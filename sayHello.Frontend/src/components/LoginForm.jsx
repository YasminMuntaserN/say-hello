import { useForm } from "react-hook-form";
import { IoMdLogIn } from "react-icons/io";
import { GoPasskeyFill } from "react-icons/go";
import Button from "../ui/Button";
import FormRow from "../ui/FormRow";
import { Link, useNavigate } from "react-router-dom";
import { useExistUser } from "../hooks/Users/useExistUser";
import SpinnerMini from "../ui/SpinnerMini";
import FormContainer from "../ui/FormContainer";
import { useUser } from "../context/UserContext";

function LoginForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset
  } = useForm();

  const {mutate,isLoading } =useExistUser();
  const navigate = useNavigate(); 
  const {login}=useUser();

  function onSubmit(data) {
    console.log(data);
    const {Email ,Password} =data;
    mutate({Email, Password},
      {
        onSuccess: (data) => {
          console.log(data);
          if(data){
          reset();
          navigate('/dashboard');
          login(data);
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
        <FormRow type="email" errors={errors} register={register} FieldName="Email" />
        <FormRow type="password" errors={errors} register={register} FieldName="Password" />
        <div className={StyledButtons}>
          <Button type="submit">
          {isLoading ?  <SpinnerMini/> :<>Login... <IoMdLogIn className="text-white text-3xl"  /></>}
          </Button>
          <Button>
            <GoPasskeyFill className="text-secondary text-xl" /> Forget Password
          </Button>
        </div>
      </form>
      <div className={StyledLine}></div>
        <p className={StyledNewAccount}>Don't Have an Account yet ?  <Link className={StyledLink} to="/signup">Crate Account</Link></p>
    </FormContainer>
  );
}

const StyledP = "text-gray-600 font-normal";
const StyledButtons = "flex justify-between gap-14 mt-10";
const StyledLine = "bg-lightText w-full h-0.5 mt-5 ";
const StyledNewAccount = "text-lg mt-10 text-center text-lightText";
const StyledLink = "text-secondary font-bold underline";

export default LoginForm;
