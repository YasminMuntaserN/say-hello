import { GoPasskeyFill } from "react-icons/go";
import { TbPasswordUser } from "react-icons/tb";
import Button from "../../ui/Button";
import Modal from "../../ui/Modal";
import ForgetPasswordForm from "./ForgetPasswordForm";
import CheckEmail from "./CheckEmail";

function ForgatPassword({withLogin=true}) {
  return (
    <Modal>
      <Modal.Open>
          {withLogin ?
          <Button>
            <GoPasskeyFill className="text-secondary text-xl" />
            Forget Password
          </Button>
          :
            <TbPasswordUser className="text-2xl cursor-pointer hover:text-purple mt-4"/>
          } 
      </Modal.Open>
      <Modal.Window>
      {withLogin ?
          <CheckEmail />
          :<ForgetPasswordForm />
      }
      </Modal.Window>
    </Modal>
  )
}

export default ForgatPassword
