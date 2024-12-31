import Modal from "../../ui/Modal";
import { MdGroupAdd } from "react-icons/md";
import CreateGroupForm from "./CreateGroupForm";


function CreateGroup() {
  return (
    <Modal>
      <Modal.Open opens="group">
        <MdGroupAdd className={StyledIcon}/>
      </Modal.Open>
      <Modal.Window name="group">
        <CreateGroupForm/>
      </Modal.Window>
      
    </Modal>
  )
}
const StyledIcon = "text-3xl text-lightText hover:text-purple cursor-pointer";
export default CreateGroup
