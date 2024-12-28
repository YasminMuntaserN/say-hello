import FormRow from "./FormRow"

function PasswordFormRow({register, errors ,label}) {
  return (
<FormRow 
  type="password" 
  errors={errors} 
  register={register} 
  FieldName="Password"
  rules={{
    required: "New password is required",
    validate: {
      hasUppercase: (value) =>
        /[A-Z]/.test(value) || "Password must contain at least one uppercase letter",
      hasLowercase: (value) =>
        /[a-z]/.test(value) || "Password must contain at least one lowercase letter",
      hasNumber: (value) =>
        /[0-9]/.test(value) || "Password must contain at least one number",
      hasSpecialChar: (value) =>
        /[!@#$%^&*(),.?":{}|<>]/.test(value) || "Password must contain at least one special character",
      isLongEnough: (value) =>
        value.length >= 8 || "Password must be at least 8 characters long",
    },
  }} 
  label={label}
/>
  )
}

export default PasswordFormRow
