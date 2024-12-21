import { useUser } from "../../context/UserContext";
import AddIcon from "../../ui/AddIcon";
import { GrSubtract } from "react-icons/gr";
function AddFriend() {
  const {showUsers , setShowUsers}=useUser();

  return (
    <div className="flex justify-between mt-5 mx-10">
      <p className="text-3xl font-semibold">Chats</p>
      {!showUsers?
      <AddIcon size="lg" handleOnClick={()=>setShowUsers(s=>!s)}/>
      :<GrSubtract className="text-3xl " onClick={()=>setShowUsers(s=>!s)}/>
      }
    </div>
  )
}

export default AddFriend
