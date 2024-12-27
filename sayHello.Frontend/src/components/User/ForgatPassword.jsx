import { GoPasskeyFill } from "react-icons/go";
import Button from "../../ui/Button";
import Modal from "../../ui/Modal";
import ForgetPasswordForm from "./ForgetPasswordForm";

function ForgatPassword() {
  return (
    <Modal>
      <Modal.Open>
          <Button>
            <GoPasskeyFill className="text-secondary text-xl" /> Forget Password
          </Button>
      </Modal.Open>
      <Modal.Window>
          <ForgetPasswordForm />
      </Modal.Window>
    </Modal>
  )
}

export default ForgatPassword
