import { IoSettingsSharp } from "react-icons/io5";
import Modal from "../../ui/Modal";
import { useChat } from "../../context/UserContext";
import EditUserForm from "./EditUserForm";

function EditUserInfo() {
  const { user } = useChat();
  
  return (
    <Modal>
      <Modal.Open opens="edit">
        <button>
          <IoSettingsSharp className="text-2xl hover:text-purple" />
        </button>
      </Modal.Open>

      <Modal.Window name="edit">
          <EditUserForm user={user} />
      </Modal.Window>
    </Modal>
  );
}

export default EditUserInfo;