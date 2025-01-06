const messages = [
  "Hello !",
  "How Are You ?",
  "As-salamu alaykum 👋",
];

function OptionsMessages({ addMessage }) {


  const handleSend = (message) => {
    if (message.trim()) {
      addMessage(message);
    }
  };
  return (
    <div className={optionsContainer}>
      {messages.map((message, index) => (
        <div
          key={index}
          className={StyledMessage}
          onClick={() => handleSend(message)}
        >
          {message}
        </div>
      ))}
    </div>
  );
}

const StyledMessage =
  "bg-gradient-message cursor-pointer rounded-tl-full text-lg px-5 rounded-full  m-3 p-3 w-fit text-white hover:bg-gradient-to-r hover:from-blue-500 hover:bg-secondary";
const optionsContainer ="flex-grow flex justify-center items-end h-[500px]";  

export default OptionsMessages;