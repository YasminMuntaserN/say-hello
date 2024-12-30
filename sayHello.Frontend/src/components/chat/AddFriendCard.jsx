import { useChat } from "../../context/UserContext";
import AddIcon from "../../ui/AddIcon";
import Box from "../../ui/Box";
import Image from "../../ui/Image";

function AddFriendCard({ user }) {
  const {setUserInChat ,setShowChatPartnerOperations}=useChat();
  const { username, profilePictureUrl ,bio} = user;

  const OnClick =()=>{
    setUserInChat(user);
    setShowChatPartnerOperations(pre=>!pre);
  };

  return (
    <Box colsNum={3} HandleOnClick={()=>OnClick()}>
        <Image src={profilePictureUrl} alt={`${username}'s profile`} />

        <div className="ml-[-50px]  text-center">
          <p className={StyledName}>{username}</p>
          <p className="text-sm text-lightText">{bio?.length > 30 ? `${bio.substring(0, 30)}...` : bio}</p>
        </div>

        <AddIcon size="lg" handleOnClick={()=>setUserInChat(user)} />
    </Box>
  );
}

const StyledName = "text-xl text-basic font-semibold group-hover:text-white";
export default AddFriendCard;