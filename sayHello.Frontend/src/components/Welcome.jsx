import { useEffect, useRef } from "react";
import Logo from "../ui/Logo";

function Welcome() {
  const ref = useRef(null);
  const message = "Your next great conversation is just a click away – Log in and SayHello!";
  const typingSpeed = 100;

  useEffect(() => {
    let index = 0;
    const type = () => {
      if (index < message.length) {
        if (ref.current) {
          ref.current.textContent = message.substring(0, index + 1);
        }
        index++;
        setTimeout(type, typingSpeed);
      }
    };
    type(); 
  }, []);
  return (
    <div className={StyledLogo}>
    <Logo />
      <p className={StyledMessage} ref={ref}></p>
    </div>
  )
}
const StyledLogo =" flex align-items-center justify-start lg:ml-10 flex-col gap-10 py-36 ";
const StyledMessage ="text-justify font-regular text-basic font-light text-[1.8rem] leading-10 ";
export default Welcome
