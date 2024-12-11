function FormRow({type, errors, register, FieldName}) {

  return (
    <div className={StyledRow}>
     <input
        placeholder={FieldName}
        type={type} 
        {...register(FieldName, { required: `${FieldName} is required` })} 
        className={StyledInput}
      />
      {errors[FieldName] && (
        <p className="text-red-500 text-sm">{errors[FieldName]?.message}</p>
      )}
    </div>
  );
}
const StyledRow = "flex justify-between flex-nowrap my-5";
const StyledInput = "border p-3 rounded-lg w-full";


export default FormRow;
