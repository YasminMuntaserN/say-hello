function FormRow({ type, errors, register, FieldName ,value, requiredField = true ,label = false}) {

  const StyledInput = `border  ${label?"p-3 w-3/4 border-gray text-slate-500 rounded-xl focus:outline-none":"p-3 w-full rounded-lg"}`;
const StyledRow = `flex justify-between flex-nowrap ${label?"my-2" :"my-5"}`;

  const validationRules = requiredField
    ? { required: `${FieldName} is required` }
    : {};

  return (
    <div className={StyledRow}>
        {label && <label htmlFor={FieldName}>{label}</label>}
      <input
        placeholder={value ? value :FieldName}
        type={type}
        {...register(FieldName, validationRules)}
        className={StyledInput}
      />
      {errors[FieldName] && (
        <p className="text-red-500 text-sm">{errors[FieldName]?.message}</p>
      )}
    </div>
  );
}



export default FormRow;
