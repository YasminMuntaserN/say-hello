import { IoIosSend } from "react-icons/io";
function SendIcon({handleOnClick}) {
  return (
  <div className="h-14 w-14 rounded-full bg-gradient-btn flex items-center justify-center ml-[-200px]" onClick={handleOnClick}>
      <IoIosSend className={`text-white text-4xl`} />
  </div>
  )
}

export default SendIcon
