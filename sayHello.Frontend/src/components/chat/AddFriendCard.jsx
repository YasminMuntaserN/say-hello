import { useChat } from "../../context/UserContext";
import AddIcon from "../../ui/AddIcon";
import Box from "../../ui/Box";
import Image from "../../ui/Image";

function AddFriendCard({ user }) {
  const {setUserInChat ,setShowChatPartnerOperations}=useChat();
  const { username, profilePictureUrl } = user;

  const OnClick =()=>{
    setUserInChat(user);
    setShowChatPartnerOperations(pre=>!pre);
  };

  return (
    <Box colsNum={3} HandleOnClick={()=>OnClick()}>
        <Image src={profilePictureUrl} alt={`${username}'s profile`} />

        <div className="ml-[-50px]  text-center">
          <p className={StyledName}>{username}</p>
        </div>

        <AddIcon size="sm" handleOnClick={()=>setUserInChat(user)} />
    </Box>
  );
}

const StyledName = "text-xl text-basic font-semibold group-hover:text-white";
export default AddFriendCard;