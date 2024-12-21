import { useUser } from "../../context/UserContext";
import AddIcon from "../../ui/AddIcon";
import Box from "../../ui/Box";
import Image from "../../ui/Image";

function AddFriendCard({ user }) {
  const {setUserInChat}=useUser();
  const { username, profilePictureUrl } = user;

  return (
    <Box colsNum={3} HandleOnClick={()=>setUserInChat(user)}>
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