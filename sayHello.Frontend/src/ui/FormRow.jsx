function FormRow({ type, errors, register, FieldName, requiredField = true }) {
 
  const validationRules = requiredField
    ? { required: `${FieldName} is required` }
    : {};

  return (
    <div className={StyledRow}>
      <input
        placeholder={FieldName}
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
const StyledRow = "flex justify-between flex-nowrap my-5";
const StyledInput = "border p-3 rounded-lg w-full";


export default FormRow;
