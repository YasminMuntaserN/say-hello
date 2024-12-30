import { RiDeleteBinLine } from "react-icons/ri";
import Button from "../../ui/Button";
import Modal from "../../ui/Modal";
import DeleteForm from "./DeleteForm";

function DeleteUser() {
  return (
    <Modal>
      <Modal.Open opens="delete">
      <Button variant="delete">
        <RiDeleteBinLine />Delete
      </Button>
      </Modal.Open>

      <Modal.Window name="delete">
        <DeleteForm />
      </Modal.Window>
    </Modal>
  )
}

export default DeleteUser
