import { useForm } from "react-hook-form";
import { FcAddImage } from "react-icons/fc";
import FormRow from "../../ui/FormRow";
import Line from "../../ui/Line";
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";
import { useAllUsers } from "../user/hooks/useAllUsers";
import { useEffect } from "react";
import AddFriendCard from "../chat/AddFriendCard";
import { useAddGroup } from "./hooks/useGroups";
import { useGroup } from "../../context/GroupContext";


function CreateGroupForm({onClose}) {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm();
  const { mutate, AllUsers } = useAllUsers();
  const { mutate: addGroup, isLoading, error }=useAddGroup();
  const {SaveGroupMembers}=useGroup();

  function onSubmit(data) {
    console.log(data);
      if (!data) return;
      const formData = new FormData();
      formData.append("Name",data.GroupName);
      formData.append("ImageUrl", "");
    
    if (data.ProfilePicture?.[0]) {
      formData.append("photo", data.ProfilePicture[0]);
    }

    addGroup(formData, {
      onSuccess: (data) => {
        console.log("group" ,data);
        reset();
        SaveGroupMembers(data.groupId);
        onClose?.();
      }
    });
  }
  
  useEffect(()=>mutate(),[]);

  return (
  <div className="w-[700px]  p-5 ">
    <div className="border-[#6b4ca0] border-2 p-5 rounded-xl ">
    <h1 className="text-center text-secondary  font-bold text-3xl">Create Group</h1>
    {error && <p>Something happened when creating the group</p>}
    <form onSubmit={handleSubmit(onSubmit)}>
    <div className="flex items-center w-full justify-around">
      <label
        htmlFor="ProfilePicture"
        className="cursor-pointer text-5xl text-center"
      >
        <FcAddImage />
      </label>
      <input
        type="file"
        id="ProfilePicture"
        accept="image/*"
        {...register("ProfilePicture")}
        className="hidden"
      />
      <FormRow 
        type="text" 
        errors={errors} 
        register={register} 
        FieldName="GroupName" 
      />
    </div>

      <Line />
      <div className="flex-grow overflow-y-auto h-[350px] m-5">
        {
          AllUsers?.map((user) => (
            <AddFriendCard key={user.userId} user={user} groupMember={true}/>
          ))
        }
      </div>
      <div className="ml-[60%]">
      <Button variant="reset" onClick={onClose}>Cancel</Button>
      <Button variant="save" type="submit">
          {isLoading ? <SpinnerMini /> : "Create"}
      </Button>
      </div>
      </form>
    </div>
  </div>
  )
}

export default CreateGroupForm
