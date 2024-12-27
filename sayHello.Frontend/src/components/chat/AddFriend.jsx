import { GrSubtract } from 'react-icons/gr';
import { useAllUsers } from '../user/hooks/useAllUsers';
import { useChat } from '../../context/UserContext';
import AddIcon from '../../ui/AddIcon';

function AddFriend({ setUsersToShow }) {
  const { showUsers, setShowUsers } = useChat();
  const { mutate, AllUsers } = useAllUsers();

  const handleClick = () => {
    mutate();
    setUsersToShow(AllUsers);
    setShowUsers(pre=>!pre);
  };

  return (
    <div className="flex justify-between mt-5 mx-10">
      <p className="text-3xl font-semibold">Chats</p>
      {showUsers ? (
        <GrSubtract className="text-3xl" onClick={handleClick} />
      ) : (
        <AddIcon size="lg" handleOnClick={handleClick} />
      )}
    </div>
  );
}

export default AddFriend
