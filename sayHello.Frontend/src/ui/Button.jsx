function Button({children,onClick ,type="defaultStyle"}) {
 
  const className =type =="defaultStyle"? defaultStyle :type =="LargeBtn"?LargeBtn: submit;
  return (
<button className={className} onClick={onClick}>{children}</button>
  )
}

const submit ="bg-secondary p-3 rounded-2xl w-1/2 text-xl text-white flex justify-between";
const LargeBtn ="bg-secondary px-5 py-3 rounded-md w-1/2 text-center text-xl text-white my-5";
const defaultStyle ="bg-none w-1/2 font-semibold mt-3 text-secondary flex gap-2";

export default Button
