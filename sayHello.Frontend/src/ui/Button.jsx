const BUTTON_STYLES = {
  default: "bg-none w-1/2 font-semibold mt-3 text-secondary flex gap-2",
  large: "bg-secondary px-5 py-3 rounded-md w-1/2 text-center text-xl text-white my-5",
  delete: "h-12 mt-2 px-5 pt-2 rounded-xl bg-rose-200 text-rose-700 font-semibold flex justify-between gap-2 justify-items-center text-xl",
  reset: "border-lightText border-2 p-3 rounded-xl font-semibold text-center",
  save: "bg-black text-white rounded-xl p-3 m-3 font-semibold text-center",
  submit: "bg-secondary p-3 rounded-2xl w-1/2 text-xl text-white flex justify-between",
  full: "bg-none w-full border-lightText border-2 p-3 rounded-xl font-semibold text-center2",
};

function Button({ 
  children, 
  onClick, 
  type = "button", 
  variant = "default" 
}) {
  return (
    <button
      type={type}
      className={BUTTON_STYLES[variant]}
      onClick={onClick}
    >
      {children}
    </button>
  );
}

export default Button;
