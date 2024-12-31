import { RiAddFill } from "react-icons/ri";

function AddIcon({ handleOnClick, size = "lg"  }) {
  const IconSize = {
    sm: "h-5 w-5",
    lg: "h-7 w-7",
  };

  const TextSize = {
    sm: "text-xl mt-0", 
    lg: "text-3xl",         
  };

  return (
    <div
      className={`${IconSize[size]} rounded-full bg-gradient-btn flex items-center justify-center cursor-pointer`}
      onClick={handleOnClick}
    >
      <RiAddFill className={`text-white ${TextSize[size]}`} />
    </div>
  );
}

export default AddIcon;
