import { useForm } from "react-hook-form";
import FormRow from "../../ui/FormRow"
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";

function ForgetPasswordForm() {
    const {
      register,
      handleSubmit,
      formState: { errors },
      reset,
    } = useForm();
  
    function onSubmit(data) {console.log(data);}
  return (
    <div className="w-[700px]  p-5 ">
      <div className="border-[#6b4ca0] border-2 p-5 rounded-xl ">
      <img src="/password.png" alt="password" className="w-[300px] h-[300px] mt-[-160px] ml-[150px]"/>
      <h1 className="text-center text-secondary  mt-[-70px] mb-10 font-bold text-3xl">Change Password</h1>
      <form onSubmit={()=>handleSubmit(onSubmit)} >
      <FormRow 
        type="password" 
        errors={errors} 
        register={register} 
        FieldName="New password" 
        label="New password"
      />
      <FormRow 
        type="password" 
        errors={errors} 
        register={register} 
        FieldName="Confirm password" 
        label="Confirm password"
      />
      <div className="ml-[60%]">
      <Button 
            variant="reset" 
            // onClick={onClose}
          >
            Cancel
          </Button>
        <Button variant="save" type="submit">
          {/* {loading ? <SpinnerMini /> : "Save Changes"} */}
          {/* {loading ? <SpinnerMini /> : "Save Changes"} */}
          Save Changes
        </Button>
      </div>
      </form>
      </div>
    </div>
  )
}

export default ForgetPasswordForm
