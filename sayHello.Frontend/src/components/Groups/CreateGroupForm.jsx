import { useForm } from "react-hook-form";
import { FcAddImage } from "react-icons/fc";
import FormRow from "../../ui/FormRow";
import Line from "../../ui/Line";
import Button from "../../ui/Button";
import SpinnerMini from "../../ui/SpinnerMini";
import { useAllUsers } from "../user/hooks/useAllUsers";
import { useEffect, useMemo, useState } from "react";
import AddFriendCard from "../chat/AddFriendCard";
import { useAddGroup } from "./hooks/useGroups";
import { useGroup } from "../../context/GroupContext";
import { useChat } from "../../context/UserContext";
import SearchBar from "../../ui/SearchBar";


function CreateGroupForm({onClose ,groupInfo}) {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm();
  const groupId = groupInfo?.groupId ?? null;
  const groupMembers = groupInfo?.groupMembers ?? [];
  
  const { mutate, AllUsers } = useAllUsers();
  const { mutate: addGroup, isLoading, error }=useAddGroup();
  const {SaveGroupMembers ,setUpdateGroupMembers}=useGroup();
  const {setUpdatedPartnerOperations}=useChat();
  const [filteredUsers, setFilteredUsers] = useState([]); 
  const [Users, setUsers] = useState([]); 
  const AddNewMember = !!groupInfo;
  console.log(AddNewMember);

  function onSubmit(data) {
    if (!AddNewMember) {
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
        setUpdatedPartnerOperations(true);
        onClose?.();
      }
    })
    }else{
      SaveGroupMembers(groupId);
      setUpdatedPartnerOperations(true);
      setUpdateGroupMembers(true)
      onClose?.();
    } 
  }
  
  const filteredInitialUsers = useMemo(() => {
    if (!AllUsers) return [];
    return AddNewMember
      ? AllUsers.filter(user => !groupMembers.some(member => member.userId === user.userId))
      : AllUsers;
  }, [AllUsers, groupMembers, AddNewMember]);

  const handleSearch = (query) => {
    if (!query.trim()) {
      setFilteredUsers(Users);
      return;
    }
    const filtered = Users?.filter(user => 
      user.username.toLowerCase().includes(query.toLowerCase())
    );
    setFilteredUsers(filtered);
  };

  useEffect(() => {
    mutate();
  }, [mutate]);

  useEffect(() => {
    if (AllUsers?.length > 0) {
      setUsers(filteredInitialUsers);
      setFilteredUsers(filteredInitialUsers);
    }
  }, [AllUsers, filteredInitialUsers]);


  return (
  <div className="w-[700px]  p-5 ">
    <div className="border-[#6b4ca0] border-2 p-5 rounded-xl ">
    <h1 className="text-center text-secondary  font-bold text-3xl">{AddNewMember ?"Add New Member" :"Create Group"}</h1>
    {error && <p>Something happened when creating the group</p>}
    <form onSubmit={handleSubmit(onSubmit)} >
    <div className={`flex items-center w-full justify-around ${AddNewMember ? "opacity-10 pointer-events-none" : "" }`}>
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
      <SearchBar onSearch={handleSearch} />
        {
          filteredUsers?.map((user) => (
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
