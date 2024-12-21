import Image from "../../ui/Image";
import { HiDotsHorizontal } from "react-icons/hi";

function MessageHeader({receiver}) {
  const {receiverImage, receiverName, status  } = receiver;
  const statusColor= status=="Online"?"text-[#188D3F]":"text-lightTextColor";
 
  return (
    <div className={StyledContainer}>
        <div className="ml-[-500px]">
          <Image src={receiverImage} alt={`${receiverImage}'s profile`} />
        </div>
        <div className="flex flex-col">
          <p className={StyledName}>{receiverName}</p>
          <p className={statusColor}>  {status} </p>
        </div>
        <HiDotsHorizontal className="absolute right-5 top-5 text-4xl text-lightText"/>
      </div>
  )
}
const StyledContainer = "relative flex gap-5 justify-center p-2  rounded-lg shadow-2xl transition-all duration-300";
const StyledName = "text-xl text-basic font-bold";
export default MessageHeader
