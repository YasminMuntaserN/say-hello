import { FaUserLock } from "react-icons/fa6";
import { useForm } from "react-hook-form";
import Button from "../ui/Button"
import FormRow from "../ui/FormRow"
import FormContainer from "../ui/FormContainer";

function SignupForm() {
    const {
      register,
      handleSubmit,
      formState: { errors },
    } = useForm();

    function onSubmit(data) {
      console.log(data);
    };

  return (
    <FormContainer header ="Sign Up to Start Chatting!">
      <form onSubmit={handleSubmit(onSubmit)}>
        <FormRow type="email" errors={errors} register={register} FieldName="Email" />
        <FormRow type="password" errors={errors} register={register} FieldName="Password" />
        
          <Button type="submit">
            signup... <FaUserLock />
          </Button>
      </form>
      </FormContainer>
  )
}

export default SignupForm
