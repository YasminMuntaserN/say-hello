import { useChat } from "../../context/UserContext";
import Image from "../../ui/Image";
import ChatGroupOperation from "./ChatGroupOperation";
import ChatPartnerOperation from "./ChatPartnerOperation";

function ChatPartner() {
  const { userInChat } = useChat();
  const {type ,from}=userInChat;

  return (
    <div className="relative">
      <div className="absolute inset-0 bg-secondary-bg opacity-60"></div>
      <div className="relative bg-white m-3 rounded-2xl h-[87vh]">
        <div className="flex flex-col pt-[5%] pl-[30%] mb-5">
          <Image src={type?.receiverImage} size="w-28 h-28 ml-3" />
          <p className="text-secondary font-bold text-3xl mt-3">
            {type?.receiverName}
          </p>
          <p className="mt-3  text-lightText text-lg font-semibold">
            {type?.bio}
          </p>
        </div>
        <div className="px-5 mt-0">
        {from==="group"? <ChatGroupOperation groupId={type?.userId} /> : <ChatPartnerOperation />}
        </div>
      </div>
    </div>
  );
}

export default ChatPartner;
