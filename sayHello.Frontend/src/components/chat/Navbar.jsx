import Logo from "../../ui/Logo";
import { LuLogOut } from "react-icons/lu";
import { useChat } from "../../context/UserContext";
import Image from "../../ui/Image";
import EditUserInfo from "../User/EditUserInfo";
import ForgatPassword from "../User/ForgatPassword";
import { useNavigate } from "react-router-dom";
import { useLogout } from "../User/hooks/useAuth";

function Navbar() {
  const navigation =useNavigate();
  const {user ,logout}=useChat();
  const{logoutMutate}=useLogout();
  
  const handleLogout=()=>{
    logoutMutate();
    logout();
    navigation("/login");
  }
  return (
    <div className={StyledContainer}>
      <Logo size="w-[180px] ml-10" color="black"/>
      <div className={StyledIconContainer}>
      <Image src={user?.profilePictureUrl}/>
      <p className={StyledName}>{user?.username}</p>
      <EditUserInfo />
      <ForgatPassword withLogin={false}/>
      <LuLogOut className={StyledIcon} onClick={()=>handleLogout()}/>
      </div>
    </div>
  )
}

const StyledContainer="bg-gray-100 height-30 shadow-2xl transition-all duration-300 opacity-80 flex justify-between";
const StyledIconContainer="flex justify-between mt-2 mr-10 gap-5";
const StyledIcon="text-2xl cursor-pointer hover:text-purple mt-4";
const StyledName="text-xl  z-1000 font-semibold mt-4 hover:text-purple";


export default Navbar
