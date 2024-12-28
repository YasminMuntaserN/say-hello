import { BiSolidErrorCircle } from "react-icons/bi";
function FormRow({ 
  type, 
  errors, 
  register, 
  FieldName, 
  rules = {}, 
  value = "", 
  label 
}) {
  const StyledInput = `border ${label ?
          "p-3 w-3/4 border-gray text-slate-500 outline-none rounded-xl focus:outline-none flex-col" : "p-3 w-full flex-col rounded-lg"}`;
  const StyledInputRow=`flex justify-between flex-nowrap my-3 `;

  return (
    <div className={StyledRow}>
      <div className={StyledInputRow}>
      {label && <label htmlFor={FieldName} className="font-semibold">{label}</label>}
      <input
        placeholder={value || FieldName}
        id={FieldName}
        type={type}
        {...register(FieldName, rules)}
        className={StyledInput}
      />
      </div>
      {errors[FieldName] && (
        <p className="text-red-500 flex text-sm"><BiSolidErrorCircle className="mt-1 m-2" />{errors[FieldName]?.message}</p>
      )}
    </div>
  );
}
const StyledRow = "flex justify-between flex-col mt-2";
export default FormRow;