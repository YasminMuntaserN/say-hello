import { Link } from "react-router-dom";
import Logo from "../ui/Logo";
import Button from "../ui/Button";
import { useConfirmationEmail } from "../hooks/Users/useConfirmationEmail";
import { useUser } from "../context/UserContext";

function VerifyEmail() {
  const { user, logout } = useUser();
  const { mutate: conformingEmail } = useConfirmationEmail();
  

  const handleResendEmail = () => {
    if (user?.email) {
      conformingEmail(user.email);
    } else {
      console.error("No email available for confirmation.");
    }
  };

  return (
    <div className={StyledContainer}>
      <div className="bg-white p-10 lg:w-[60rem] h-fit mt-[7%] relative rounded-3xl">
        <Logo size="w-[250px] mb-5" color="black"/>
        <h1 className="text-3xl font-regular text-basic">Thank you for signing up!</h1>
        <p className="text-xl font-regular text-lightText mt-5">
          To get started, please verify your email address by<br />
          clicking the verification link we just sent to your<br />
          inbox. If you can't find the email, please check your<br />
          spam folder or click the button below to request<br />
          another verification email.
        </p>
        <Button type="LargeBtn" onClick={handleResendEmail}>Re-send verification email</Button>
        <p className="text-xl font-regular text-lightText mt-5">
          Switch to another account? 
          <Link className="text-secondary underline" to="/login" onClick={()=>logout()}>Logout</Link>
        </p>
      </div>

      <div className={StyledImageContainer}>
        <img className="md:w-[30%] sm:w-[100px] lg:w-[700px] h-auto" src="./email-security.png" alt="verifyEmail"/>
      </div>
    </div>
  );
}

const StyledContainer ="flex justify-center bg-gradient-bg opacity-80 h-screen";
const StyledImageContainer ="absolute top-10 right-10";
export default VerifyEmail;
// md:top-[25rem] md:right-[-5rem] 