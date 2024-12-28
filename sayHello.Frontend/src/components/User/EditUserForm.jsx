import { useForm } from "react-hook-form";
import { RiDeleteBinLine } from "react-icons/ri";
import { useEditUser } from "./hooks/useEditUser";
import { useChat } from "../../context/UserContext";
import Line from "../../ui/Line";
import FormRow from "../../ui/FormRow";
import Image from "../../ui/Image";
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";
import UserCard from "./UserCard";
function EditUserForm({ user, onClose }) {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm();
  
  const { mutate, loading } = useEditUser();
  const { login } = useChat();

  function onSubmit(data) {
    if (!data) return;
    const formData = new FormData();
    formData.append("Username", data.Name || user.username);
    formData.append("Email", user.email);
    formData.append("Password", data.Password || user.password);
    formData.append("Bio", data.Bio || user.bio);
    formData.append("Status", "Online");
    formData.append("ProfilePictureUrl", "");
    
    if (data.ProfilePicture?.[0]) {
      formData.append("photo", data.ProfilePicture[0]);
    } else {
      formData.append("ProfilePictureUrl", user.profilePictureUrl);
    }

    mutate({ user: formData, id: user.userId }, {
      onSuccess: (data) => {
        reset();
        login(data);
        onClose?.();
      }
    });
  }

  return (
  <div className="w-[500px]">
    <UserCard user={user} />
    <form onSubmit={handleSubmit(onSubmit)} className="p-10 mt-24">
      <Line />
      <FormRow 
        type="text" 
        errors={errors} 
        register={register} 
        FieldName="Name" 
        value={user?.username} 
        label="Name"
      />
      
      <Line />
      <FormRow 
        type="password" 
        errors={errors} 
        register={register} 
        FieldName="password" 
        value={user?.password} 
        label="Password"
      />

      <Line />
      <FormRow 
        type="text" 
        errors={errors} 
        register={register} 
        FieldName="Bio" 
        value={user?.bio} 
        label="Bio"
      />

      <Line />
      <div className="flex my-5 align-middle">
        <label htmlFor="ProfilePictureUrl" className="mr-5">
          ProfilePicture
        </label>
        <Image src={user?.profilePictureUrl} />
        <input
          type="file"
          className="m-5"
          id="ProfilePicture"
          accept="image/*"
          {...register("ProfilePicture")}
        />
      </div>
      
      <div className="flex justify-between">
      <Button variant="delete">
        <RiDeleteBinLine />Delete
      </Button>
      <div>
      <Button 
            variant="reset" 
            onClick={onClose}
          >
            Cancel
          </Button>
        <Button variant="save" type="submit">
          {loading ? <SpinnerMini /> : "Save Changes"}
        </Button>
      </div>
      </div>
    </form>
</div>

  );
}

export default EditUserForm;
