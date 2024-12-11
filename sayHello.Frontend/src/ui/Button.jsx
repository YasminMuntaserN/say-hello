function Button({children,onClick ,type="defaultStyle"}) {
 
  const className =type =="defaultStyle"? defaultStyle: submit;
  return (
<button className={className} onClick={onClick}>{children}</button>
  )
}

const submit ="bg-secondary p-3 rounded-2xl w-1/2 text-xl text-white flex justify-between";
const defaultStyle ="bg-none w-1/2 font-semibold mt-3 text-secondary flex gap-2";

export default Button
